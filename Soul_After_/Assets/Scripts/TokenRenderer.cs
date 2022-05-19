using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenRenderer : MonoBehaviour
{
    public Text TokenNo;
    public FloatValue Token;

    private Animator anim;
    private int reward;

    void Start()
    {
        anim = GetComponent<Animator>();
        TokenNo.text = "X " + Token.initialValue.ToString();
    }

    public void BounceToken()
    {
        anim.SetTrigger("Bounce");
    }
}
