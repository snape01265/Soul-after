using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//RPG Talk Holder ���� ������Ʈ�� Activate or Deactivate �ϴ� ��ũ��Ʈ
public class DeactivateTalk : MonoBehaviour
{
    public string previousCutscene;
    public string watchedCutscene;
    public CutsceneList cutsceneList;

    private void Update()
    {
        if (!cutsceneList.initialValue.Contains(watchedCutscene))
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
