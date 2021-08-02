using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneOrdered : MonoBehaviour
{
    public PlayableDirector cutscene;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public string cutsceneName;
    public CutsceneList cutsceneList;
    public List<string> cutsceneOrder;
    private List<string> cutList;

    void Start()
    {
        cutscene = GetComponent<PlayableDirector>();
        cutList = new List<string>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        cutList = cutsceneOrder.GetRange(0, cutsceneOrder.IndexOf(cutsceneName));
        Debug.Log(cutList.ToString());
        foreach (string str in cutList)
        {
            Debug.Log("str = " + str);
            if(!cutsceneList.initialValue.Contains(str))
            {
                // this part doesnt work.
                // why????
                Debug.Log("reached return");
                return;
            }
        }

        if (!cutsceneList.initialValue.Contains(cutsceneName) && other.CompareTag("Player"))
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(FadeCo());
            cutsceneList.initialValue.Add(cutsceneName);
        }
    }
    public void StartCutscene()
    {

        if (!cutsceneList.initialValue.Contains(cutsceneName))
        {
            StartCoroutine(FadeCo());
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
        cutscene.Play();
    }
}
