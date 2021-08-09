using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRenderer : MonoBehaviour
{
    public BoolList buttonStates;
    public BoolList boxOnBtn;
    private GameObject BtnUp;
    private bool BtnState;
    private int BtnIdx;

    private void Start()
    {
        //Debug.Log("this is =" + this.gameObject.name);
        char a = this.gameObject.name[this.gameObject.name.Length - 1];
        //Debug.Log("char is =" + a.ToString());
        BtnIdx = int.Parse(a.ToString()) - 1;
        BtnState = buttonStates.initialValue[BtnIdx];
        BtnUp = this.transform.Find("Button Up").gameObject;

        if (BtnState)
        {
            // if true, disable Button Up sprite
            ButtonDown();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // disable Button Up Sprite
        // also change ButtonState accordingly
        if (collision.GetComponent<Collider2D>().CompareTag($"MovableObject"))
        {
            boxOnBtn.initialValue[BtnIdx] = true;
            if(BtnUp.activeSelf)
            {
                ButtonDown();
                gameObject.GetComponentInParent<SajaPuzzleBehavior>().AdvancePuzzle(BtnIdx);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag($"MovableObject"))
        {
            boxOnBtn.initialValue[BtnIdx] = false;
        }
    }

    public void ButtonUp()
    {
        BtnUp.SetActive(true);
    }

    public void ButtonDown()
    {
        BtnUp.SetActive(false);
    }
}
