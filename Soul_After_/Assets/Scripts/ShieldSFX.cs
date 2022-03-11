using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSFX : MonoBehaviour
{
    public AudioSource ShieldBroken;
    public AudioSource ShieldRecharge;
    
    public void BrokenSFX()
    {
        ShieldBroken.Play();
    }
    public void RechargeSFX()
    {
        ShieldRecharge.Play();
    }
}
