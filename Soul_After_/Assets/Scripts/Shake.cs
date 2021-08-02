using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//카메라 흔들리는 스크립트

public class Shake : MonoBehaviour
{
    public Animator camAnim;

    public void CamShake()
    {
        camAnim.SetTrigger("Shake");
    }
}
