using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HitEffect : MonoBehaviour
{
    private Light2D lightEffect;
    float t = 0.2f;

    void Start()
    {
        lightEffect = gameObject.GetComponent<Light2D>();
        StartCoroutine(flashNow());
    }
    public IEnumerator flashNow()
    {
        float waitTime = t / 2;
        while (lightEffect.intensity < 1)
        {
            lightEffect.intensity += Time.deltaTime / waitTime;
            yield return null;
        }
        while (lightEffect.intensity > 0)
        {
            lightEffect.intensity -= Time.deltaTime / waitTime;
            yield return null;
        }
        yield return null;
    }
}
