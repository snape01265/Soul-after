using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SpikeTrap : MonoBehaviour
{
    public int patternType;
    public float interval;
    public float scale;
    public AudioSource SpikeSFX;

    public float CD = .5f;
    private readonly float BUFFER = .5f;
    private readonly float ANIMDUR = .5f;

    private BoxCollider2D Collider2D;
    private Animator Animator;

    private void Start()
    {
        Collider2D = GetComponent<BoxCollider2D>();
        Animator = GetComponent<Animator>();

        switch (patternType)
        {
            case 0:
                {
                    Animator.SetBool("Up", true);
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
        yield return new WaitForSeconds(CD);
        while (true)
        {
            yield return new WaitForSeconds(BUFFER);
            Collider2D.enabled = true;
            yield return new WaitForSeconds(sec - BUFFER - ANIMDUR * 2);
            Animator.SetBool("Up", false);
            yield return new WaitForSeconds(ANIMDUR);
            Collider2D.enabled = false;
            yield return new WaitForSeconds(sec + CD * 2);
            Animator.SetBool("Up", true);
            if (SpikeSFX)
                SpikeSFX.Play();
            yield return new WaitForSeconds(ANIMDUR);
        }
    }

    IEnumerator BlinkPatternEven(float sec)
    {
        while (true)
        {
            Animator.SetBool("Up", false);
            yield return new WaitForSeconds(ANIMDUR);
            Collider2D.enabled = false;
            yield return new WaitForSeconds(sec + CD * 2);
            Animator.SetBool("Up", true);
            if (SpikeSFX)
                SpikeSFX.Play();
            yield return new WaitForSeconds(ANIMDUR);
            yield return new WaitForSeconds(BUFFER);
            Collider2D.enabled = true;
            yield return new WaitForSeconds(sec - BUFFER - ANIMDUR * 2);
        }
    }
}
