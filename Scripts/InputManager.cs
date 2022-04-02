using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action<char> OnInput;

    private void Awake()
    {
        ServiceLocator.Register(this);
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
                OnInput?.Invoke(key);
            }
        }        
    }
}
