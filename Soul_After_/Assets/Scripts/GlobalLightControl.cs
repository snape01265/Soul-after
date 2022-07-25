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
        GlobalLight.color = new Color(.4f, .4f, 1f);
    }

    public void ResetAtmosphere()
    {
        GlobalLight = GetComponent<Light2D>();
        GlobalLight.color = new Color(1f, 1f, 1f);
    }
}
