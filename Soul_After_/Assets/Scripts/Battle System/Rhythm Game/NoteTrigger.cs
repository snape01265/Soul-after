using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTrigger : MonoBehaviour
{
    public bool bePressed;
    public KeyCode keyToPress;

    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            if(bePressed)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Flower")
        {
            bePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Flower")
        {
            bePressed = false;
        }
    }
}