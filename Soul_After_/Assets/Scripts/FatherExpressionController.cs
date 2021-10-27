using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGTALK;

public class FatherExpressionController : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Sigh", true);
    }
}
