using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//RPG Talk Area ���� ������Ʈ�� enable or disable �ϴ� ��ũ��Ʈ 
public class DisableTalk : MonoBehaviour
{
    public RPGTalkArea[] rpgTalk;
    public int[] index;
    public void EnableAllObject()
    {
        foreach (RPGTalkArea i in rpgTalk)
        {
            i.gameObject.SetActive(true);
        }
    }
    public void DisableAllObject()
    {
        foreach (RPGTalkArea i in rpgTalk)
        {
            i.gameObject.SetActive(false);
        }
    }
    public void EnableIndexObject()
    {
        foreach (int i in index)
        {
            rpgTalk[i].gameObject.SetActive(true);
        }
    }
    public void DisableIndexObject()
    {
        foreach (int i in index)
        {
            rpgTalk[i].gameObject.SetActive(false);
        }
    }
}
