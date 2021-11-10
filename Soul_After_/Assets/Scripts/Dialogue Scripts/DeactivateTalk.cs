using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//RPG Talk Holder 게임 오브젝트를 Activate or Deactivate 하는 스크립트
public class DeactivateTalk : MonoBehaviour
{
    public string previousCutscene;
    public string thisCutscene;
    public CutsceneList cutsceneList;

    private void Start()
    {
        CheckCutscene();
    }
    public void CheckCutscene()
    {
        if (!cutsceneList.initialValue.Contains(thisCutscene) && cutsceneList != null)
        {
            if(GetComponent<Collider2D>() != null)
            {
                GetComponent<Collider2D>().enabled = cutsceneList.initialValue.Contains(previousCutscene);
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
