using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public AudioSource _audio;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        if(_audio != null)
        {
            _audio.Play();
        }
        playerStorage.initialValue = playerPosition;
        StartCoroutine(FadeCo());
        if (GameObject.Find("Music Player").GetComponent<MusicPlayer>() != null)
        {
            GameObject.Find("Music Player").GetComponent<MusicPlayer>().UpdateMusic(sceneToLoad);
        }
    }

    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        Time.timeScale = 1;
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void SetplayerPositionX(float x)
    {
        playerPosition = new Vector2(x, playerPosition.y);
    }

    public void SetplayerPositionY(float y)
    {
        playerPosition = new Vector2(playerPosition.x, y);
    }

    public void SetsceneToLoad(string sceneName)
    {
        sceneToLoad = sceneName;
    }
}
