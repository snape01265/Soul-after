using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRenderer : MonoBehaviour
{
    public BoolList buttonStates;
    private GameObject BtnUp;
    private bool BtnState;
    private int BtnIdx;

    void Start()
    {
        if (!buttonStates.initialValue.Equals(buttonStates.defaultValue))
        {
            buttonStates.initialValue = new List<bool>(buttonStates.defaultValue);
        }

        Debug.Log("this is =" + this.gameObject.name);
        char a = this.gameObject.name[this.gameObject.name.Length - 1];
        Debug.Log("char is =" + a.ToString());
        BtnIdx = int.Parse(a.ToString()) - 1;
        BtnState = buttonStates.initialValue[BtnIdx];

        if (BtnState)
        {
            // if true, disable Button Up sprite
            BtnUp = this.transform.Find("Button Up").gameObject;
            BtnUp.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // disable Button Up Sprite
        // also change ButtonState accordingly

        BtnUp = this.transform.Find("Button Up").gameObject;

        if (BtnUp.activeSelf && collision.GetComponent<Collider2D>().CompareTag($"MovableObject"))
        {
            Debug.Log(this.name + "is pressed!");
            Debug.Log("BtnIdx = " + BtnIdx);
            BtnUp.SetActive(false);
            buttonStates.initialValue[BtnIdx] = true;
        }

        if (this.gameObject.CompareTag($"ResetBtn") && collision.GetComponent<Collider2D>().CompareTag($"Player"))
        {
            Debug.Log("Reset");
            buttonStates.initialValue = new List<bool>(buttonStates.defaultValue);
            InitBtn();
        }
    }

    private void InitBtn()
    {
        BtnUp = this.transform.Find("Button Up").gameObject;
        BtnState = buttonStates.initialValue[BtnIdx];

        if (BtnState)
        {
            BtnUp.SetActive(false);
        } else
        {
            BtnUp.SetActive(true);
        }
    }

    public void ButtonUp()
    {
        BtnUp = this.transform.Find("Button Up").gameObject;
        BtnUp.SetActive(true);
        buttonStates.initialValue[BtnIdx] = false;
    }
}
