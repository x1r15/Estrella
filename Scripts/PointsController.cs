using System;
using System.Collections;
using UnityEngine;

public class PointsController : MonoBehaviour, IControlledGameService
{
    [SerializeField]
    private float _yearLength = 1f;
    
    private Coroutine _increaseYearJob;

    public int Year { get; private set; } = DateTime.Now.Year;

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

    public void Reset()
    {
        Year = DateTime.Now.Year;
        OnNewYear?.Invoke(Year);
        Enable();
    }

    private IEnumerator IncreaseYear()
    {
        Year++;
        OnNewYear?.Invoke(Year);
        yield return new WaitForSeconds(_yearLength);
        _increaseYearJob = StartCoroutine(IncreaseYear());
    }
}
