using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTransition : MonoBehaviour
{
    private AudioSource _audio;
    public float duration;
    public float targetVolume;
    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        yield break;
    }
    public void FadeMusic()
    {
        _audio = GameObject.Find("Music Player").GetComponent<AudioSource>();
        StartCoroutine(StartFade(_audio, duration, targetVolume));
    }
}
