using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    public GameObject warningZone;

    public void Destroy()
    {
        Destroy(warningZone);
    }
}
