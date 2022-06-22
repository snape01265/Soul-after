using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlameTrap : MonoBehaviour
{
    public AudioSource FlameSFX;
    public int patternType;
    public float interval;
    public float scale;
    public float BUFFER = .5f;

    public Vector3 NormFire = new Vector3(1f, 1f, 1f);
    public Vector3 SmolFire = new Vector3(.1f, .1f, .1f);

    private Light2D lightComp;
    private CircleCollider2D circleCollider2D;

    private void Start()
    {
        lightComp = transform.Find("Point Light 2D").GetComponent<Light2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        switch (patternType)
        {
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
                    StartCoroutine(Breath(scale));
                    break;
                }
            case 4:
                {
                    Shrink();
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
            circleCollider2D.enabled = true;
            yield return new WaitForSeconds(sec - BUFFER);
            Shrink();
            circleCollider2D.enabled = false;
            yield return new WaitForSeconds(sec);
            Expand();
            yield return new WaitForSeconds(BUFFER);
        }
    }

    IEnumerator BlinkPatternEven(float sec)
    {
        while (true)
        {
            Shrink();
            circleCollider2D.enabled = false;
            yield return new WaitForSeconds(sec);
            Expand();
            yield return new WaitForSeconds(BUFFER);
            circleCollider2D.enabled = true;
            yield return new WaitForSeconds(sec - BUFFER);
        }
    }

    IEnumerator Breath(float scale)
    {
        Vector3 scaledVec = Vector3.one * scale;

        while (true)
        {
            while (transform.localScale.x < 2 && transform.localScale.y < 2)
            {
                lightComp.pointLightOuterRadius = Mathf.Clamp(lightComp.pointLightOuterRadius + scale * Time.deltaTime, 0f, 2f);
                transform.localScale += scaledVec * Time.deltaTime;
                yield return null;
            }
            
            while (transform.localScale.x > 0 && transform.localScale.y > 0)
            {
                lightComp.pointLightOuterRadius = Mathf.Clamp(lightComp.pointLightOuterRadius - scale * Time.deltaTime, 0f, 2f);
                transform.localScale -= scaledVec * Time.deltaTime;
                yield return null;
            }

            circleCollider2D.enabled = false;
            yield return new WaitForSeconds(1f);
            circleCollider2D.enabled = true;
        }
    }

    public void Shrink()
    {
        lightComp.pointLightOuterRadius = .1f;
        transform.localScale = SmolFire;
    }

    public void Expand()
    {
        if (FlameSFX)
            FlameSFX.Play();
        lightComp.pointLightOuterRadius = 1f;
        transform.localScale = NormFire;
    }
}
