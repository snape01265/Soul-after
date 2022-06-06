using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLightControl : MonoBehaviour
{
    private Light2D GlobalLight;
    public void NightAtmosphere()
    {
        GlobalLight = GetComponent<Light2D>();
        Color NightColor = GlobalLight.color;
        NightColor.r = 0.4f;
        NightColor.g = 0.4f;
        NightColor.b = 1f;
    }
}
