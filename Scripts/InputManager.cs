using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private SoundManager _soundManager;
    public event Action<char> OnInput;

    private void Awake()
    {
        ServiceLocator.Register(this);
    }

    private void Start()
    {
        _soundManager = ServiceLocator.Get<SoundManager>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        var inputString = Input.inputString;
        if (!string.IsNullOrEmpty(inputString))
        {
            foreach (var key in inputString)
            {
                _soundManager.Play(SoundManager.Sounds.Click);
                OnInput?.Invoke(key);
            }
        }        
    }
}
