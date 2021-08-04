using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRenderer : MonoBehaviour
{
    public BoolList ButtonStates;

    void Start()
    {
        char a = this.gameObject.ToString()[this.gameObject.ToString().Length - 1];
        bool btnstate = ButtonStates.initialValue[int.Parse(a.ToString())-1];
        if (btnstate)
        {
            // if true, disable Button Up sprite
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // disable Button Up Sprite
        // also change ButtonState accordingly
    }
}
