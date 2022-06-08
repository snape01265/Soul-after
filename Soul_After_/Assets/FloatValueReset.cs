using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatValueReset : MonoBehaviour
{
    public FloatValue floatValue;

    public void ResetStage()
    {
        floatValue.initialValue = 0;
    }
}
