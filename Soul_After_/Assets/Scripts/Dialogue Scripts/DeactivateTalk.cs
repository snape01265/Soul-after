using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//RPG Talk Holder ���� ������Ʈ�� Activate or Deactivate �ϴ� ��ũ��Ʈ
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
