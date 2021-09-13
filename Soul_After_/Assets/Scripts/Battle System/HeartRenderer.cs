using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRenderer : MonoBehaviour
{
    private bool hpState;
    private int idx;
    public AudioSource _audio;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        char a = this.gameObject.name[this.gameObject.name.Length - 1];
        idx = int.Parse(a.ToString()) - 1;
    }

    public void HPGain()
    {
        _audio.Play();
        anim.SetBool("Dmg", false);
    }

    public void HPLoss()
    {
        _audio.Play();
        anim.SetBool("Dmg", true);
    }
}
