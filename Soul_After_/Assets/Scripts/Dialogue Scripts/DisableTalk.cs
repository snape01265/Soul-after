using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//RPG Talk Area 게임 오브젝트를 enable or disable 하는 스크립트 
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
