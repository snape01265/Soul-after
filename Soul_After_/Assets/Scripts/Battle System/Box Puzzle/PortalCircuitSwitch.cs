using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCircuitSwitch : MonoBehaviour
{
    public GameObject[] PortalPairs;
    public AudioSource switchSFX;

    private Animator anim;
    private bool isRange = false;
    private Collider2D playerCol;
    private bool isStartingPair = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerCol = GameObject.Find("Player").GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (isRange && Input.GetButtonDown("Jump"))
        {
            if (switchSFX != null)
                switchSFX.Play();
            CircuitChange();
            if (anim.GetBool("Flicked") == false)
                anim.SetBool("Flicked", true);
            else if (anim.GetBool("Flicked") == true)
                anim.SetBool("Flicked", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isRange = false;
    }

    private void CircuitChange()
    {
        if (isStartingPair)
        {
            isStartingPair = false;
            for (int i = 0; i < PortalPairs.Length; i += 2)
            {
                PortalPairs[i].SetActive(false);
                PortalPairs[i + 1].SetActive(true);
            }
        }
        else
        {
            isStartingPair = true;
            for (int i = 0; i < PortalPairs.Length; i += 2)
            {
                PortalPairs[i].SetActive(true);
                PortalPairs[i + 1].SetActive(false);
            }
        }
    }
}

