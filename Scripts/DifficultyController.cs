using System.Collections;
using UnityEngine;

public class DifficultyController : MonoBehaviour, IControlledGameService
{
    [SerializeField]
    private float _increaseDifficultyEverySeconds = 3f;
    
    private Vocabulary _vocabulary;
    private MeteorSpawner _meteorSpawner;
    private Coroutine _increaseDifficultyLoopJob;
    
    private void Awake()
    {
        ServiceLocator.Register(this);
    }
    
    void Start()
    {
        _vocabulary = ServiceLocator.GetSafe<Vocabulary>();
        _meteorSpawner = ServiceLocator.Get<MeteorSpawner>();
    }

    public void IncreaseDifficulty()
    {
        _vocabulary.MaxLength += 1;
        _meteorSpawner.MinInitialForce += 1;
        _meteorSpawner.MaxInitialForce += 1;
    }

    public IEnumerator DifficultyLoop()
    {
        yield return new WaitForSeconds(_increaseDifficultyEverySeconds);
        IncreaseDifficulty();
        _increaseDifficultyLoopJob = StartCoroutine(DifficultyLoop());
    }

    public void Enable()
    {
        _increaseDifficultyLoopJob = StartCoroutine(DifficultyLoop());
    }

    public void Disable()
    {
        if (_increaseDifficultyLoopJob != null)
        {
            StopCoroutine(_increaseDifficultyLoopJob);
        }
    }

    public void Reset()
    {
        Enable();
    }
}
