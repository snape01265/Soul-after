using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCircuitSwitch : MonoBehaviour
{
    public GameObject[] PortalPairs;
    public AudioSource switchSFX;

    private Animator anim;
    private bool pressed = false;
    private Collider2D playerCol;
    private bool isStartingPair = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerCol = GameObject.Find("Player").GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && gameObject.GetComponent<Collider2D>().IsTouching(playerCol))
        {
            if (switchSFX != null)
                switchSFX.Play();
            pressed = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Collider2D>().CompareTag("Player"))
        {
            if (pressed)
            {
                pressed = false;
                CircuitChange();
                if (anim.GetBool("Flicked") == false)
                    anim.SetBool("Flicked", true);
                else if (anim.GetBool("Flicked") == true)
                    anim.SetBool("Flicked", false);
            }
        }
    }

    private void CircuitChange()
    {
        if (isStartingPair)
        {
            isStartingPair = false;
            for (int i = 0; i < PortalPairs.Length; i += 2)
            {
                PortalPairs[i].SetActive(true);
                PortalPairs[i + 1].SetActive(false);
            }
        }
        else
        {
            isStartingPair = true;
            for (int i = 0; i < PortalPairs.Length; i += 2)
            {
                PortalPairs[i].SetActive(false);
                PortalPairs[i + 1].SetActive(true);
            }
        }
    }
}

