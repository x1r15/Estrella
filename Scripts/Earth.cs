using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Earth : MonoBehaviour
{
    private int _hp = 100;

    private bool IsDestroyed => _hp <= 0;

    public event EventHandler<HealthChangeEventArgs> OnHealthChange;
    public event Action OnDestroyed;

    private Animator _animator;
    private Transform _selfTransform;
    private CircleCollider2D _collider;
    
    private static readonly int DamagedTrigger = Animator.StringToHash("Damaged");
    private static readonly int DestroyedTrigger = Animator.StringToHash("Destroyed");
    private static readonly int ResetTrigger = Animator.StringToHash("Reset");
    
    [SerializeField]
    private float _rotationSpeed = -5;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private GameObject _rocket;

    private void Awake()
    {
        ServiceLocator.Register(this);
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CircleCollider2D>();
        _selfTransform = transform;
    }

    private void Update()
    {
        _selfTransform.Rotate(new Vector3(0, 0, _rotationSpeed * Time.deltaTime));
    }

    public void Damage(int amount)
    {
        _hp = Mathf.Clamp(_hp - amount, 0, 100);
        _animator.SetTrigger(DamagedTrigger);
        OnHealthChange?.Invoke(this, new HealthChangeEventArgs {DamageTaken = amount, HpLeft = _hp});

        if (_hp <= 0)
        {
            Destroy();
        }
    }

    [ContextMenu("Destroy")]
    private void Destroy()
    {
        OnDestroyed?.Invoke();
        StartCoroutine(SpawnExplosion());
        _animator.SetTrigger(DestroyedTrigger);
    }

    [ContextMenu("Reset")]
    public void Reset()
    {
        _hp = 100;
        OnHealthChange?.Invoke(this, new HealthChangeEventArgs {DamageTaken = 0, HpLeft = _hp});
        _animator.SetTrigger(ResetTrigger);
    }

    public void InitUltimateSuperUberAttack()
    {
        StartCoroutine(UltimateSuperUberAttack());
    }

    [ContextMenu("SuperUberAttack")]
    private IEnumerator UltimateSuperUberAttack()
    {
        var meteors = Meteor.All.ToList();
        foreach (var meteor in meteors)
        {
            var rocket = Instantiate(_rocket, GetRandomEarthPosition(), Quaternion.identity);
            var rocketScript = rocket.GetComponent<Rocket>();
            rocketScript.Init(meteor.transform);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator SpawnExplosion()
    {
        while (IsDestroyed)
        {
            var point = Random.insideUnitCircle;
            var targetPosition = 
                _selfTransform.position.ToVector2() + point * _collider.radius;
            Instantiate(_explosion, targetPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private Vector2 GetRandomEarthPosition()
    {
        var point = Random.insideUnitCircle;
        return _selfTransform.position.ToVector2() + point * _collider.radius;
    }
}
