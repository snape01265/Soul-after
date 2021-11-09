using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public StringValue lastScene;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadNextScene();
        }
    }
    public void LoadNextScene()
    {
        lastScene.initialValue = sceneToLoad;
        playerStorage.initialValue = playerPosition;
        StartCoroutine(FadeCo());
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
