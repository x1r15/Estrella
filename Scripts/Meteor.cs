using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Meteor : MonoBehaviour, IAction
{
    public static List<Meteor> All = new List<Meteor>();
    
    private Rigidbody2D _rigidbody;
    private Earth _earth;
    
    private int _damage;
    private float _initialForce;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField]
    private SpriteRenderer _spriteFgRenderer;
    
    [SerializeField]
    private SpriteRenderer _spriteBgRenderer;

    [SerializeField]
    private GameObject _rocket;

    [SerializeField]
    private GameObject _explosion;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        All.Add(this);
        _earth = ServiceLocator.Get<Earth>();
        SetInitialDirection();
    }

    public void Act()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void SetInitialDirection()
    {
        var point = Random.insideUnitCircle;
        var targetPosition = 
            _earth.transform.position.ToVector2() + point * _earth.GetComponent<CircleCollider2D>().radius;
        var dir = (targetPosition - transform.position.ToVector2()).normalized;
        _rigidbody.AddForce(dir * _initialForce);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject == _earth.gameObject)
        {
            _earth.Damage(_damage);
            Destroy();
        }
    }

    public void Destroy()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        All.Remove(this);
    }

    public void Init(float initialForce, int damage, SpriteSet spriteSet)
    {
        _initialForce = initialForce;
        _damage = damage;
        _spriteRenderer.sprite = spriteSet.Sprite;
        _spriteFgRenderer.sprite = spriteSet.Fg;
        _spriteBgRenderer.sprite = spriteSet.Bg;
    }
}

