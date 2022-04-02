using System;
using UnityEngine;

public class Earth : MonoBehaviour
{
    private int _hp = 100;

    public event EventHandler<DamageEventArgs> OnDamage;

    private Animator _animator;
    private Transform _selfTransform;
    
    private static readonly int DamagedTrigger = Animator.StringToHash("Damaged");
    private static readonly int DestroyedTrigger = Animator.StringToHash("Destroy");
    
    [SerializeField]
    private float _rotationSpeed = -5;

    private void Awake()
    {
        ServiceLocator.Register(this);
        _animator = GetComponent<Animator>();
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
        OnDamage?.Invoke(this, new DamageEventArgs {DamageTaken = amount, HpLeft = _hp});

        if (_hp <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        Debug.Log("Destroyed");
        _animator.SetTrigger(DestroyedTrigger);
    }
}
