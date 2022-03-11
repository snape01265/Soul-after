using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightControl : MonoBehaviour
{
    public float duration;
    private Light2D light2D;
    void Start()
    {
        light2D = this.GetComponent<Light2D>();
    }
    public void IntensityControl(float targetIntensity)
    {
        StartCoroutine(Intensity(light2D, duration, targetIntensity));
    }
    private IEnumerator Intensity(Light2D light2D, float duration, float targetIntensity)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            light2D.intensity = Mathf.Lerp(light2D.intensity, targetIntensity, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
