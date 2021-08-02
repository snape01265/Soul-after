using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderButton: MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public int dialogueSentence;
    public StringValue lastScene;
    void Awake()
    {
        if (fadeInPanel != null)
        {
            lastScene.initialValue = sceneToLoad;
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }
    public void LoadNextScene()
    {
        if (dialogueSentence == 0)
        {
            playerStorage.initialValue = playerPosition;
            StartCoroutine(FadeCo());
        }
        dialogueSentence--;
    }
    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
