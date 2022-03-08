using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invismaze_Goal : MonoBehaviour
{
    public InvismazePuzzleManager PuzzleManager;
    private bool _isGoal = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isGoal)
        {
            _isGoal = true;
            PuzzleManager.FinPuzzle();
            StartCoroutine(FlipGoal());
        }
    }

    IEnumerator FlipGoal()
    {
        yield return new WaitForSeconds(.1f);
        _isGoal = false;
    }
}
