using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invismaze_FloorIsLava : MonoBehaviour
{
    public InvismazePuzzleManager PuzzleManager;
    public AudioSource FloorEnterSFX;
    private bool _isLava = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isLava)
        {
            if (FloorEnterSFX)
                FloorEnterSFX.Play();
            _isLava = true;
            PuzzleManager.ReturnToStart();
            StartCoroutine(FlipLava());
        }
    }

    IEnumerator FlipLava()
    {
        yield return new WaitForSeconds(.1f);
        _isLava = false;
    }
}
