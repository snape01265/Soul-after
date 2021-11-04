using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRenderer : MonoBehaviour
{
    public AudioSource _audio;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void HPGain()
    {
        if(_audio)
        {
            _audio.Play();
        }

        anim.SetBool("Dmg", false);
    }

    public void HPLoss()
    {
        if(_audio)
        {
            _audio.Play();
        }
        
        anim.SetBool("Dmg", true);
    }
}
