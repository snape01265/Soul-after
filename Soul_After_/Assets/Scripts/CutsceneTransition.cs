using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//�� Timeline���� �ٸ� Timeline���� Transition�� �� ���Ǵ� ��ũ��Ʈ 
public class CutsceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject fadeOutPanel;
    public float fadeWait;

    private void OnEnable()
    {
        StartCoroutine(FadeCo());
        GameObject.Find("Music Player").GetComponent<MusicPlayer>().UpdateMusic(sceneToLoad);
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
