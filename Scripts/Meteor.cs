using UnityEngine;
using Random = UnityEngine.Random;

public class Meteor : MonoBehaviour, IAction
{
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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _earth = ServiceLocator.Get<Earth>();
        SetInitialDirection();
    }

    public void Act()
    {
        var rocket = Instantiate(_rocket, _earth.transform.position, Quaternion.identity);
        rocket.GetComponent<Rocket>().Init(transform);
        
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
            Destroy(gameObject);
        }
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

