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
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public StringValue lastScene;
    public AudioSource _audio;
    public Text sceneName;

    void Awake()
    {
        if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
        SceneName();
    }

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
        lastScene.initialValue = sceneToLoad;
        playerStorage.initialValue = playerPosition;
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
    public void SceneName()
    {
        //GameObject.Find("SceneNameText").GetComponent<Animator>().SetBool("FadeShort", true);
        if (sceneName != null) 
        {
            var sn = sceneName.GetComponent<Animator>();
            sn.SetBool("FadeShort", true);
        }
    }
}
