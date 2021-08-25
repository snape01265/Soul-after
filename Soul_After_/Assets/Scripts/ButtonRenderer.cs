using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRenderer : MonoBehaviour
{
    private SajaPuzzleBehavior saja;
    private GameObject BtnUp;
    private bool BtnState;
    private int BtnIdx;
    public AudioSource _audio;

    private void Awake()
    {
        saja = this.gameObject.GetComponentInParent<SajaPuzzleBehavior>();

        char a = this.gameObject.name[this.gameObject.name.Length - 1];
        BtnIdx = int.Parse(a.ToString()) - 1;
        BtnUp = this.transform.Find("Button Up").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // disable Button Up Sprite
        // also change ButtonState accordingly
        if (collision.GetComponent<Collider2D>().CompareTag($"MovableObject"))
        {
            saja.ToggleBoxOnBtn(BtnIdx);
            if(BtnUp.activeSelf)
            {
                Debug.Log("Pressed.");
                ButtonDown();
                gameObject.GetComponentInParent<SajaPuzzleBehavior>().AdvancePuzzle(BtnIdx);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag($"MovableObject"))
        {
            saja.ToggleBoxOnBtn(BtnIdx);
        }
    }

    public void ButtonUp()
    {
        _audio.Play();
        BtnUp.SetActive(true);
    }

    public void ButtonDown()
    {
        _audio.Play();
        BtnUp.SetActive(false);
    }
}
