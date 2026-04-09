using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    CANDLE_ON,
    CANDLE_OFF,
    FLASHLIGHT_ON,
    FLASHLIGHT_OFF,
    PUZZLE_COMPLETE
}

public class SoundCollection
{
    private AudioClip clip;

    public SoundCollection(string clipNames)
    {
        this.clip = Resources.Load<AudioClip>(clipNames);
        if (clip == null)
        {
            Debug.LogError("Invalid Clip");
        }
    }
    public AudioClip GetClip()
    {
        return clip;
    }
}
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public float mainVolume = 1.0f;
    private Dictionary<SoundType, SoundCollection> sounds;
    private AudioSource audioSource;
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        sounds = new()
        {
            { SoundType.CANDLE_ON, new SoundCollection("light") },
            { SoundType.CANDLE_OFF, new SoundCollection("extinguish") },
            { SoundType.FLASHLIGHT_ON, new SoundCollection("flashlight-on") },
            { SoundType.FLASHLIGHT_OFF, new SoundCollection("flashlight_off") }
        };
    }
    
    public static void Play(SoundType type, AudioSource audioSrc = null, float pitch = -1)
    {
        if(Instance.sounds.ContainsKey(type))
        {
            audioSrc ??= Instance.audioSource;
            audioSrc.volume = UnityEngine.Random.Range(0.7f, 1.0f) * Instance.mainVolume;
            audioSrc.pitch = pitch >= 0 ? pitch : UnityEngine.Random.Range(0.75f, 1.25f);
            audioSrc.clip = Instance.sounds[type].GetClip();
            audioSrc.Play();
        }    
    }
}
