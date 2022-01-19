using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlameLane : MonoBehaviour
{
    public Flameshot[] flameShots;
    public float flameIntervalTime;
    IEnumerator ShootFlame()
    {
        foreach (Flameshot flame in flameShots)
        {
            flame.FireFlames();
            yield return new WaitForSeconds(flameIntervalTime);
        }
    }

    public void FireLane()
    {
        StartCoroutine(ShootFlame());

    }
}
