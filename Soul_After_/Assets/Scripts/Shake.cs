using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ī�޶� ��鸮�� ��ũ��Ʈ

public class Shake : MonoBehaviour
{
    public Animator camAnim;

    public void CamShake()
    {
        camAnim.SetTrigger("Shake");
    }
}
