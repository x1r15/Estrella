using System;
using System.Collections;
using UnityEngine;

public class PointsController : MonoBehaviour, IControlledGameService
{
    [SerializeField]
    private float _yearLength = 1f;
    
    private Coroutine _increaseYearJob;
    private int _year = 2020;

    public event Action<int> OnNewYear;

    private void Awake()
    {
        ServiceLocator.Register(this);
    }

    public void Enable()
    {
        _increaseYearJob = StartCoroutine(IncreaseYear());
    }

    public void Disable()
    {
        if (_increaseYearJob != null)
        {
            StopCoroutine(_increaseYearJob);
        }
    }

    private IEnumerator IncreaseYear()
    {
        _year++;
        OnNewYear?.Invoke(_year);
        yield return new WaitForSeconds(_yearLength);
        _increaseYearJob = StartCoroutine(IncreaseYear());
    }
}
