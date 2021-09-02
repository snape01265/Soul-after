using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textAnimator : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeShort()
    {
        anim.SetBool("FadeShort", true);
        anim.SetBool("Gameover", false);
        anim.SetBool("FadeLong", false);
    }
}
