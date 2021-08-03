using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//RPG Talk Holder 게임 오브젝트를 Activate or Deactivate 하는 스크립트
public class DeactivateTalk : MonoBehaviour
{
    public string previousCutscene;
    public string watchedCutscene;
    public CutsceneList cutsceneList;

    private void Update()
    {
        if (!cutsceneList.initialValue.Contains(watchedCutscene))
        {
            if (cutsceneList.initialValue.Contains(previousCutscene))
            {
                GetComponent<Collider2D>().enabled = true;
            }
            else
            {
                GetComponent<Collider2D>().enabled = false;
            }
        }
        else
        {
            Deactivate();
        }
    }
    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
