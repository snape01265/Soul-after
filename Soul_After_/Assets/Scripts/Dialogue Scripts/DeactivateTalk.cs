using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//RPG Talk Holder 게임 오브젝트를 Activate or Deactivate 하는 스크립트
public class DeactivateTalk : MonoBehaviour
{
    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
