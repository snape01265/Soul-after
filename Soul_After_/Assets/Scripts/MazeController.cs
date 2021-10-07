using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGTALK;

public class MazeController : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Sigh", true);
    }
}
