using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitShake : MonoBehaviour
{
    private Shake shake;

    void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    private void Update()
    {

    }
}
