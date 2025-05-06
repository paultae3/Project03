using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    // these coroutines ensure we can restart any 'over time' processes
    private Coroutine _lerpVolumeRoutine = null;
    private Coroutine _stopRoutine = null;

    private void Awake()
    {
        // setup our AudioSources
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.playOnAwake = true;
    }

    public void Play(AudioClip musicTrack, float fadeTime)
    {
        // start at 0 volume so we can fade up over time
        _audioSource.volume = 0;
        _audioSource.clip = musicTrack;
        _audioSource.Play();
        // fade up the volume
        FadeVolume(MusicManager.Instance.Volume, fadeTime);
    }
    public void Stop(float fadeTime)
    {
        // reset a stop coroutine, if it's already going
        if (_stopRoutine != null)
            StopCoroutine(_stopRoutine);
        _stopRoutine = StartCoroutine(StopRoutine(fadeTime));
    }
    public void FadeVolume(float targetVolume, float fadeTime)
    {
        // animate each audiosource.volume over time
        targetVolume = Mathf.Clamp(targetVolume, 0, 1);
        if (fadeTime < 0) fadeTime = 0;

        if (_lerpVolumeRoutine != null)
            StopCoroutine(_lerpVolumeRoutine);
        _lerpVolumeRoutine = StartCoroutine
            (LerpVolumeRoutine(targetVolume, fadeTime));
    }
    private IEnumerator StopRoutine(float fadeTime)
    {
        // cancel current running volume fades, stop prematurely
        if (_lerpVolumeRoutine != null)
            StopCoroutine(_lerpVolumeRoutine);
        _lerpVolumeRoutine = StartCoroutine(LerpVolumeRoutine(0, fadeTime));

        // wait for blend to finish
        yield return _lerpVolumeRoutine;
        _audioSource.Stop();
    }
    IEnumerator LerpVolumeRoutine(float targetVolume, float fadeTime)
    {
        float newVolume;
        float startVolume = _audioSource.volume;
        for (float elapsedTime = 0; elapsedTime <= fadeTime; elapsedTime += Time.deltaTime)
        {
            newVolume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeTime);
            _audioSource.volume = newVolume;

            yield return null;
        }
        // if we've made it this far, set to target for accuracy
        _audioSource.volume = targetVolume;
    }
}