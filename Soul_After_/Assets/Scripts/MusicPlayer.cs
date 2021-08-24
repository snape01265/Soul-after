using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//다른 scene에서도 지속적으로 음악을 플레이하게 하는 스크립트
public class MusicPlayer : MonoBehaviour
{
    public CutsceneList bgm;
    public float fadeoutTime;
    private AudioSource curMusic;

    private void Awake()
    {
        curMusic = gameObject.GetComponent<AudioSource>();
        SetUpSingleton();
    }
    public void UpdateMusic(string scenetoload)
    {
        string scene = scenetoload;
        if (bgm != null && bgm.initialValue.Contains(scene))
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("Not Destroyed");
        }
        else
        {
            StartCoroutine(MusicFade());
            Debug.Log("Destroyed");
        }

    }
    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public IEnumerator MusicFade()
    {
        float currentTime = 0;
        float start = curMusic.volume;

        while (currentTime < fadeoutTime)
        {
            currentTime += Time.deltaTime;
            curMusic.volume = Mathf.Lerp(start, 0, currentTime / fadeoutTime);
            yield return null;
        }
        Destroy(gameObject);
        yield break;
    }
    public void DisableMusic()
    {
        gameObject.SetActive(false);
    }
}
