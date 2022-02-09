using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCircuitSwitch : MonoBehaviour
{
    public GameObject[] PortalPairs;

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
            PortalPairs[0].SetActive(false);
            PortalPairs[1].SetActive(false);
            PortalPairs[2].SetActive(true);
            PortalPairs[3].SetActive(true);

        } else
        {
            isStartingPair = true;
            PortalPairs[0].SetActive(true);
            PortalPairs[1].SetActive(true);
            PortalPairs[2].SetActive(false);
            PortalPairs[3].SetActive(false);
        }
    }
}
