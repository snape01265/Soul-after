using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SajaPuzzleReset : MonoBehaviour
{
    public VectorList birdPos;
    private SajaPuzzleBehavior SajaPuzzle;
    private Animator anim;
    private bool button_down_jump;
    private bool button_up_jump;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        SajaPuzzle = gameObject.GetComponentInParent<SajaPuzzleBehavior>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Collider2D>().CompareTag("Player"))
        {
            if (Input.GetButtonDown("Jump") && anim.GetBool("Flicked") == false)
            {
                button_down_jump = true;
                anim.SetBool("Flicked", true);
                Debug.Log("Reset!");
                SajaPuzzle.ResetPuzzle();
            }
            else if (Input.GetButtonDown("Jump") && anim.GetBool("Flicked") == true)
            {
                button_down_jump = true; 
                anim.SetBool("Flicked", false);
                Debug.Log("Reset!");
                SajaPuzzle.ResetPuzzle();
            }
        }
        else
        {
            Debug.Log("Reset Failed.");
        }
    }
}
