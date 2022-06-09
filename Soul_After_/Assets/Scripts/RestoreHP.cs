using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHP : MonoBehaviour
{
    [SerializeField]
    public FloatValue playerHealth;

    public void RefillHP()
    {
        playerHealth.initialValue = playerHealth.defaultValue;
    }
}
