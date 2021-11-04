using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlameTrap : MonoBehaviour
{
    public int patternType;
    public float interval;
    public float scale;

    private readonly Vector3 NORMALFIRE = new Vector3(1f, 1f, 1f);
    private readonly Vector3 SMALLFIRE = new Vector3(.1f, .1f, .1f);

    private Light2D lightComp;

    private void Start()
    {
        lightComp = transform.Find("Point Light 2D").GetComponent<Light2D>();

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
            yield return new WaitForSeconds(sec);
            Shrink();
            yield return new WaitForSeconds(sec);
            Expand();
        }
    }

    IEnumerator BlinkPatternEven(float sec)
    {
        while (true)
        {
            Shrink();
            yield return new WaitForSeconds(sec);
            Expand();
            yield return new WaitForSeconds(sec);
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

            GetComponent<CircleCollider2D>().enabled = false;
            yield return new WaitForSeconds(1f);
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    private void Shrink()
    {
        lightComp.pointLightOuterRadius = .1f;
        transform.localScale = SMALLFIRE;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    private void Expand()
    {
        lightComp.pointLightOuterRadius = 1f;
        transform.localScale = NORMALFIRE;
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
