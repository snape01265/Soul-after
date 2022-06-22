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
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PushBox"))
        {
            box.goalCount -= 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PushBox") && (box.isAvailable || box.isReset))
        {
            box.goalCount += 1;
        }
    }
}