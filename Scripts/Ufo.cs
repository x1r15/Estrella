using UnityEngine;

public class Ufo : MonoBehaviour, IAction
{
    public static Ufo Instance { get; private set; }
    
    [SerializeField]
    private GameObject _explosion;
    
    private float _speed;

    private LifeText _lifeText;
    private Earth _earth;
    private SoundManager _soundManager;
    private AudioSource _usedAudioSource;
    private float _xLimit;

    private void Awake()
    {
        _lifeText = GetComponent<LifeText>();
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }
    
    private void Start()
    {
        _lifeText.Init("ufo");
        _earth = ServiceLocator.Get<Earth>();
        _soundManager = ServiceLocator.Get<SoundManager>();
        _usedAudioSource = _soundManager.PlayInLoop(SoundManager.Sounds.Ufo);
        _speed = Mathf.Clamp(Screen.width / 150f, 2f, 8f);
    }

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime, 0f, 0f);
        if (transform.position.x > _xLimit) Destroy(gameObject);
    }

    public void Init(float xLimit)
    {
        _xLimit = xLimit;
    }

    public void Act()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        _soundManager.Play(SoundManager.Sounds.Explosion);
        _earth.InitUltimateSuperUberAttack();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (_usedAudioSource != null) _usedAudioSource.Stop();
    }
}
