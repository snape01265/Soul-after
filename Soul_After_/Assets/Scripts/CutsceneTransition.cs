using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//�� Timeline���� �ٸ� Timeline���� Transition�� �� ���Ǵ� ��ũ��Ʈ 
public class CutsceneTransition : MonoBehaviour
{
    public string sceneToLoad;
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
    private void OnEnable()
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
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
