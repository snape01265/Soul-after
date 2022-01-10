using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SpikeTrap : MonoBehaviour
{
    public int patternType;
    public float interval;
    public float scale;

    private readonly float BUFFER = .5f;

    private BoxCollider2D Collider2D;
    private Animator Animator;

    private void Start()
    {
        Collider2D = GetComponent<BoxCollider2D>();

        switch (patternType)
        {
            case 0:
                {
                    break;
                }
            case 1:
                {
                    StartCoroutine(BlinkPatternOdd(interval));
                    break;
                }
            case 2:
                {
                    StartCoroutine(BlinkPatternEven(interval));
                    break;
                }
            case 3:
                {
                    break;
                }
            case 4:
                {
                    break;
                }
            case 5:
                {
                    break;
                }
        }
    }

    IEnumerator BlinkPatternOdd(float sec)
    {
        while (true)
        {
            yield return new WaitForSeconds(sec - BUFFER);
            Collider2D.enabled = true;
            yield return new WaitForSeconds(BUFFER);
            Collider2D.enabled = false;
            yield return new WaitForSeconds(sec);
        }
    }

    IEnumerator BlinkPatternEven(float sec)
    {
        while (true)
        {
            Collider2D.enabled = false;
            yield return new WaitForSeconds(sec);
            yield return new WaitForSeconds(sec - BUFFER);
            Collider2D.enabled = true;
            yield return new WaitForSeconds(BUFFER);
        }
    }
}
