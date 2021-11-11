using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//RPG Talk Holder ���� ������Ʈ�� Activate or Deactivate �ϴ� ��ũ��Ʈ
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
