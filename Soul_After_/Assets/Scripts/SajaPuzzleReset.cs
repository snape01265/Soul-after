using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SajaPuzzleReset : MonoBehaviour
{
    public VectorList birdPos;
    private SajaPuzzleBehavior SajaPuzzle;

    private void Awake()
    {
        SajaPuzzle = gameObject.GetComponentInParent<SajaPuzzleBehavior>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Player") && Input.GetButtonDown("Jump"))
        {
            SajaPuzzle.ResetPuzzle();
        }
    }
}
