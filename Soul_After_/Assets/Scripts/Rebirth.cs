using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
//환생할 때 빛을 환하게 하느 스크립트 
public class Rebirth : MonoBehaviour
{
    private Bloom bloom = null;
    public float bloomIntensity;
    public float speed;
    public int cases;

    private void Start()
    {
        Volume volume = GetComponent<Volume>();
        volume.sharedProfile.TryGet<Bloom>(out bloom);
    }

    private void FixedUpdate()
    {
        if (cases == 1)
        {
            bloomIntensity += speed * Mathf.Lerp(1, bloomIntensity, Time.deltaTime);
            if (bloom != null)
            {
                bloom.intensity.SetValue(new NoInterpMinFloatParameter(bloomIntensity, 0, true));
            }
        }
        else if (cases == 2)
        {
            bloomIntensity -= 4 * speed * Mathf.Lerp(1, bloomIntensity, Time.deltaTime);
            if (bloom != null)
            {
                bloom.intensity.SetValue(new NoInterpMinFloatParameter(bloomIntensity, 0, true));
            }
        }
        else if (cases == 3)
        {
            bloomIntensity = 1;
            if (bloom != null)
            {
                bloom.intensity.SetValue(new NoInterpMinFloatParameter(bloomIntensity, 0, true));
            }
        }
    }

    public void ChangeBloomIntensityUp()
    {
        cases = 1;
    }
    public void ChangeBloomIntensityDown()
    {
        cases = 2;
    }
    public void SetBloomIntensity()
    {
        cases = 3;
    }
}

