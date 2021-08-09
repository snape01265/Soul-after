using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//비석과 큰길에서 사용되는 뮤직 플레이어
public class MusicPlayer : MonoBehaviour
{
    public CutsceneList bgm;
    public float fadeoutTime;
    private AudioSource curMusic;

    private void Awake()
    {
        curMusic = this.gameObject.GetComponent<AudioSource>();
        SetUpSingleton();
    }
    public void UpdateMusic(string scenetoload)
    {
        string scene = scenetoload;
        if (bgm.initialValue.Contains(scene))
        {
            DontDestroyOnLoad(this.gameObject);
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
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
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
        Destroy(this.gameObject);
        yield break;
    }
}
