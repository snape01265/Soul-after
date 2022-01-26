using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_FullHealth : MonoBehaviour
{
    public FloatValue HealthObj;

    void Start()
    {
        HealthObj.initialValue = 4;
    }
}
