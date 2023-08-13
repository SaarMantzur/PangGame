using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _fireAudioSource;
    [SerializeField] private AudioSource _openGameAudioSource;
    [SerializeField] private AudioSource _splitAudioSource;
    [SerializeField] private AudioSource _lostAudioSource;

    private void Awake()
    {
        EventsManager.FireEvent.AddListener(_fireAudioSource.Play);
        EventsManager.StartGameEvent.AddListener((level)=>_openGameAudioSource.Play());
        EventsManager.SplitEvent.AddListener((ball)=>_splitAudioSource.Play());
        EventsManager.EndGameEvent.AddListener(_lostAudioSource.Play);
    }
}
