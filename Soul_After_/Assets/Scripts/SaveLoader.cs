using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���Ǵ� Scene: Start Menu
// ���: �̾��ϱ� ���, ����� ������ ������� ��ȯ
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
