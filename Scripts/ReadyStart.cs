using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyStart : MonoBehaviour
{
    private SoundManager _soundManager;

    private void Start()
    {
        _soundManager = ServiceLocator.Get<SoundManager>();
    }
    
    //Played by the animation
    private void Ready()
    {
        _soundManager.Play(SoundManager.Sounds.Ready);
    }

    //Played by the animation
    private void Type()
    {
        _soundManager.Play(SoundManager.Sounds.Type);
    }
}
