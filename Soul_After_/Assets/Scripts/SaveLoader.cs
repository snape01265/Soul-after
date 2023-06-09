using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 사용되는 Scene: Start Menu
// 기능: 이어하기 기능, 저장된 마지막 장면으로 전환
public class SaveLoader : MonoBehaviour
{
    public StringValue lastScene;
    public VectorValue playerStorage;
    public GameObject fadeOutPanel;
    public float fadeWait;

    public void LoadNextScene()
    {
        Time.timeScale = 1f;
        StartCoroutine(FadeCo());
        if (GameObject.Find("Music Player").GetComponent<MusicPlayer>() != null)
        {
            GameObject.Find("Music Player").GetComponent<MusicPlayer>().UpdateMusic(lastScene.initialValue);
        }
    }

    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(lastScene.initialValue);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
