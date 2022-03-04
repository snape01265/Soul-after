using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invismaze_FloorIsLava : MonoBehaviour
{
    public InvismazePuzzleManager PuzzleManager;
    private bool _isLava = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isLava)
        {
            _isLava = true;
            PuzzleManager.TeletoStart();
            StartCoroutine(FlipLava());
        }
    }

    IEnumerator FlipLava()
    {
        yield return new WaitForSeconds(.1f);
        _isLava = false;
    }
}
