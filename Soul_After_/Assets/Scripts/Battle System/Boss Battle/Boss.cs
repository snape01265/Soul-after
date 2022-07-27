using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public float attackDuration;
    public GameObject[] bossPhases;
    [HideInInspector]
    public GameObject turrets;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public bool cooldown = false;
    [HideInInspector]
    public bool phaseChange = false;
    [HideInInspector]
    public SpriteRenderer stunEffect;
    public bool damaged;
    public float fadeTime;
    public AudioSource sfx;

    private Fadein fade;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        fade = GameObject.Find("Fadein").GetComponent<Fadein>();
    }

    private void Update()
    {
        if(cooldown)
        {
            anim.SetBool("Vulnerable", true);
        }
        else
        {
            anim.SetBool("Vulnerable", false);
        }
        if(phaseChange)
        {
            StopAllCoroutines();
            fade.FadeInOutStatic(fadeTime);
            damaged = false;
            turrets.SetActive(false);
            StartCoroutine(EnableBoss());
            phaseChange = false;
        }
    }
    public void Stun()
    {
        if(cooldown)
        {
            if (sfx)
                sfx.Play();
            StartCoroutine(BossStun());
        }
    }
    public IEnumerator BossStun()
    {
        anim.SetBool("Stunned", true);
        bossPhases[anim.GetInteger("Phase") - 1].SetActive(false);
        stunEffect.enabled = true;
        cooldown = false;
        fade.FadeInOutStatic(fadeTime);
        yield return new WaitForSeconds(fadeTime);
        turrets.SetActive(true);
        damaged = true;
        yield return new WaitForSeconds(attackDuration);

        if (sfx)
            sfx.Play();

        fade.FadeInOutStatic(fadeTime);
        damaged = false;
        turrets.SetActive(false);
        yield return new WaitForSeconds(fadeTime + 0.5f);
        anim.SetBool("Stunned", false);
        stunEffect.enabled = false;
        bossPhases[anim.GetInteger("Phase") - 1].SetActive(true);
    }
    public IEnumerator EnableBoss()
    {
        yield return new WaitForSeconds(fadeTime + 0.5f);
        anim.SetBool("Stunned", false);
        bossPhases[anim.GetInteger("Phase") - 1].SetActive(true);
    }
}
