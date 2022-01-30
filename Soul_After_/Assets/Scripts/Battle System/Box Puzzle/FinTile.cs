using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinTile : MonoBehaviour
{
    private PushBoxPuzzleManager box;
    void Start()
    {
        box = GameObject.Find("PushBoxPuzzleManager").GetComponent<PushBoxPuzzleManager>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("PushBox"))
        {
            box.goalReached = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("PushBox"))
        {
            box.goalReached = false;
        }
    }
}
