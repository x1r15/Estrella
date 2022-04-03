using System;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Transform _target;
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private float _thrust;

    [SerializeField]
    private GameObject _explosion;

    public void Init(Transform target)
    {
        _target = target;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        try
        {
            var relativePos = _target.position - transform.position;
            var desiredUp = new Vector3(relativePos.y, -relativePos.x, 0) * Mathf.Sign(-relativePos.x);
            var rotation = Quaternion.LookRotation(-Vector3.forward, desiredUp);
            transform.rotation = rotation;
            _rigidbody.AddForce(_thrust * relativePos.normalized, ForceMode2D.Force);
        }
        catch (MissingReferenceException _)
        {
            Destroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == _target.gameObject)
        {
            Destroy(_target.gameObject);
            Destroy();
        }
    }

    private void Destroy()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
