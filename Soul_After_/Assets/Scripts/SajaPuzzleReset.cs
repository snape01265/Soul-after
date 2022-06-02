using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SajaPuzzleReset : MonoBehaviour
{
    private SajaPuzzleBehavior SajaPuzzle;
    private Animator anim;
    private bool pressed = false;
    private Collider2D playerCol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        SajaPuzzle = gameObject.GetComponentInParent<SajaPuzzleBehavior>();
        playerCol = GameObject.Find("Player").GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump") && gameObject.GetComponent<Collider2D>().IsTouching(playerCol))
        {
            pressed = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Collider2D>().CompareTag("Player"))
        {
            if (pressed && anim.GetBool("Flicked") == false)
            {
                anim.SetBool("Flicked", true);
                SajaPuzzle.ResetPuzzle();
                pressed = false;
            }
            else if (pressed && anim.GetBool("Flicked") == true)
            {
                anim.SetBool("Flicked", false);
                SajaPuzzle.ResetPuzzle();
                pressed = false;
            }
        }
    }
}
