using System.Collections;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour, IControlledGameService
{
    public float SpawningRate = 2f;
    public float MinInitialForce = 10f;
    public float MaxInitialForce = 20f;

    [SerializeField]
    private GameObject _meteor;

    [SerializeField]
    private MeteorSettings _smallMeteor;
    [SerializeField]
    private MeteorSettings _mediumMeteor;
    [SerializeField]
    private MeteorSettings _largeMeteor;

    private Vector3 _positionZero;
    private Vector3 _positionScreenEnd;
    private Coroutine _spawningJob;

    private void Awake()
    {
        ServiceLocator.Register(this);
    }
    private void Start()
    {
        _positionZero = 
            Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height + 150))
                .WithAxis(Axis.Z, 0);
        _positionScreenEnd = 
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height + 150))
                .WithAxis(Axis.Z, 0);
        
    }

    private IEnumerator SpawnMeteor()
    {
        var position = Vector3.Lerp(_positionZero, _positionScreenEnd, Random.value);
        var meteor = Instantiate(_meteor, position, Quaternion.identity);
        SetupMeteor(meteor);
        yield return new WaitForSeconds(SpawningRate);
        _spawningJob = StartCoroutine(SpawnMeteor());
    }

    private void SetupMeteor(GameObject obj)
    {
        var term = SetupLifeText(obj);
        var meteor = obj.GetComponent<Meteor>();
        var settings = GetSettings(term);
        var spriteSet = settings.GetRandomSpritesSet();
        meteor.Init(
            Random.Range(MinInitialForce, MaxInitialForce) * settings.SpeedModificator,
            settings.Damage,
            spriteSet);
    }

    private MeteorSettings GetSettings(string term)
    {
        if (term.Length <= 6)
        {
            return _smallMeteor;
        }
        if (term.Length <= 15)
        {
            return _mediumMeteor;
        }
        return _largeMeteor;
    }

    private string SetupLifeText(GameObject obj)
    {
        var term = ServiceLocator.GetSafe<Vocabulary>().GetRandomTerm();
        obj.GetComponent<LifeText>().Init(term);
        return term;
    }

    public void Enable()
    {
        _spawningJob = StartCoroutine(SpawnMeteor());
    }

    public void Disable()
    {
        if (_spawningJob != null)
        {
            StopCoroutine(_spawningJob);            
        }

    }
}
