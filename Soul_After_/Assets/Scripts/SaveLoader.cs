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
