using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private MusicPlayer _musicPlayer1;
    private MusicPlayer _musicPlayer2;
    private MusicPlayer _activeMusicPlayer = null;

    AudioClip _activeMusicTrack;

    float _volume = 0.2f;
    public float Volume
    {
        get => _volume;
        private set
        {
            value = Mathf.Clamp(value, 0, 1);
            _volume = value;
        }
    }

    private static MusicManager _instance;
    public static MusicManager Instance
    {
        get
        {
            // Lazy Instantiation
            if (_instance == null)
            {
                // search the scene to see if it exists
                _instance = FindAnyObjectByType<MusicManager>();
                if (_instance == null)
                {
                    // create a MusicManager from scratch
                    GameObject singletonGO = new GameObject("MusicManager_singleton");
                    _instance = singletonGO.AddComponent<MusicManager>();

                    DontDestroyOnLoad(singletonGO);
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // this is our music player, set it up
        SetupMusicPlayers();
    }

    void SetupMusicPlayers()
    {
        _musicPlayer1 = gameObject.AddComponent<MusicPlayer>();
        _musicPlayer2 = gameObject.AddComponent<MusicPlayer>();
        _activeMusicPlayer = _musicPlayer1;
    }

    public void Play(AudioClip musicTrack, float fadeTime)
    {
        if (musicTrack == null) return;
        if (musicTrack == _activeMusicTrack) return;

        if (_activeMusicTrack != null)
            _activeMusicPlayer.Stop(fadeTime);

        _activeMusicTrack = musicTrack;

        SwitchActiveMusicPlayer();
        // now that we've switched, fade in on the new player
        _activeMusicPlayer.Play(musicTrack, fadeTime);
    }
    public void Stop(float fadeTime)
    {
        if (_activeMusicTrack == null)
            return;

        _activeMusicTrack = null;
        _activeMusicPlayer.Stop(fadeTime);
    }
    private void SwitchActiveMusicPlayer()
    {
        // if it's 1 make it 2, if 2 make it 1
        if (_activeMusicPlayer == _musicPlayer1)
            _activeMusicPlayer = _musicPlayer2;

        else if (_activeMusicPlayer == _musicPlayer2)
            _activeMusicPlayer = _musicPlayer1;
    }
}