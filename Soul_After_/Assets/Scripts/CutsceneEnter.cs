using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// Timeline을 작동 시키는 스크립트
public class CutsceneEnter : MonoBehaviour
{
    public PlayableDirector cutscene;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public string cutsceneName;
    public CutsceneList cutsceneList;
    void Start()
    {
        if (cutsceneList.initialValue.Contains(cutsceneName))
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        cutscene = GetComponent<PlayableDirector>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!cutsceneList.initialValue.Contains(cutsceneName) && other.CompareTag("Player"))
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(FadeCo());
            AddCutscene();
        }   
    }
    public void StartCutscene()
    {
        if (!cutsceneList.initialValue.Contains(cutsceneName))
        {
            StartCoroutine(FadeCo());
            AddCutscene();
        }
    }
    public void AddCutscene()
    {
        if (!cutsceneList.initialValue.Contains(cutsceneName))
        {
            cutsceneList.initialValue.Add(cutsceneName);
        }
    }
    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            GameObject fadeOut = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(fadeOut, fadeWait);
            yield return new WaitForSeconds(fadeWait);
        }
        //cutscene.Play();
    }
}
