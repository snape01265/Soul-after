using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//RPG Talk Area ���� ������Ʈ�� enable or disable �ϴ� ��ũ��Ʈ 
public class DisableTalk : MonoBehaviour
{
    public RPGTalkArea rpgTalk;
    public void EnableThisObject()
    {
        rpgTalk.gameObject.SetActive(true);
    }
    public void DisableThisObject()
    {
        rpgTalk.gameObject.SetActive(false);
    }
}
