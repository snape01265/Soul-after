using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SajaPuzzleReset : MonoBehaviour
{
    public VectorList birdPos;
    private SajaPuzzleBehavior SajaPuzzle;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Player"))
        {
            SajaPuzzle = gameObject.GetComponentInParent<SajaPuzzleBehavior>();

            if (SajaPuzzle != null)
            {
                SajaPuzzle.ResetPuzzle();
            }
        }
    }
}
