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
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }

    }
    public void LoadNextScene()
    {
        StartCoroutine(FadeCo()); 
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
