using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//RPG Talk Holder 게임 오브젝트를 Activate or Deactivate 하는 스크립트
public class DeactivateTalk : MonoBehaviour
{
    public string previousCutscene;
    public string watchedCutscene;
    public CutsceneList cutsceneList;

    private void Start()
    {
        CheckCutscene();
    }
    private void CheckCutscene()
    {
        if (!cutsceneList.initialValue.Contains(watchedCutscene) && cutsceneList != null)
        {
            if (cutsceneList.initialValue.Contains(previousCutscene))
            {
                if(GetComponent<Collider2D>() != null)
                {
                    GetComponent<Collider2D>().enabled = true;
                }
            }
            else
            {
                if (GetComponent<Collider2D>() != null)
                {
                    GetComponent<Collider2D>().enabled = false;
                }
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
