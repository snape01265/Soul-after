using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// Timeline�� �۵� ��Ű�� ��ũ��Ʈ
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
        cutsceneList.initialValue.Add(cutsceneName);
    }
    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            GameObject fadeOut = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(fadeOut, fadeWait);
            yield return new WaitForSeconds(fadeWait);
        }
        cutscene.Play();
    }
}
