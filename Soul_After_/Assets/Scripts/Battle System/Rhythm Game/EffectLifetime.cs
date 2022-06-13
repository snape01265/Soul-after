using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EffectLifetime : MonoBehaviour
{
    public float lifetime = 1f;

    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
