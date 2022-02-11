using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinTile_1 : MonoBehaviour
{
    private PushBoxPuzzleManager_1 box;

    void Start()
    {
        box = GameObject.Find("PushBoxPuzzleManager").GetComponent<PushBoxPuzzleManager_1>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("PushBox"))
        {
            box.goalCount -= 1;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("PushBox"))
        {
            box.goalCount += 1;
        }
    }
}