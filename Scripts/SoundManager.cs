using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] _audioSources;

    [SerializeField]
    private List<Sound> _sounds;

    private void Awake()
    {
        _audioSources = GetComponents<AudioSource>();
        ServiceLocator.Register(this);
    }

    public AudioSource Play(Sounds sound, bool loop = false)
    {
        var availableSource = _audioSources.FirstOrDefault(source => !source.isPlaying);
        if (availableSource == null) return null;
        availableSource.clip = _sounds.Find(s => s.Name == sound).Clip;
        availableSource.loop = loop;
        availableSource.Play();
        return availableSource;
    }

    public AudioSource PlayInLoop(Sounds sound)
    {
        return Play(sound, true);
    }

    [Serializable]
    public class Sound
    {
        public Sounds Name;
        public AudioClip Clip;
    }

    public enum Sounds
    {
        Explosion,
        Click,
        RocketLaunch,
        Ufo,
        EarthDestroyed,
        ButtonClicked,
        Ready,
        Type
    }
}
