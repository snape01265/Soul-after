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
    public bool damaged;
    public float fadeTime;
    public AudioSource sfx;

    private Fadein fade;
    private BoxCollider2D boxCollider2D;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        fade = GameObject.Find("Fadein").GetComponent<Fadein>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(cooldown)
        {
            anim.SetBool("Vulnerable", true);
            boxCollider2D.enabled = true;
        }
        else
        {
            anim.SetBool("Vulnerable", false);
            boxCollider2D.enabled = false;
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
            boxCollider2D.enabled = false;
        }
    }
    public IEnumerator BossStun()
    {
        anim.SetBool("Stunned", true);
        bossPhases[anim.GetInteger("Phase") - 1].SetActive(false);
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
        bossPhases[anim.GetInteger("Phase") - 1].SetActive(true);
    }
    public IEnumerator EnableBoss()
    {
        yield return new WaitForSeconds(fadeTime + 0.5f);
        anim.SetBool("Stunned", false);
        bossPhases[anim.GetInteger("Phase") - 1].SetActive(true);
    }
}
