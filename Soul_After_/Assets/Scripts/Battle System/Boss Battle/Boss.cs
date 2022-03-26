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
    public float fadeTime;
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
    }
    public void Stun()
    {
        if(cooldown)
        {
            StartCoroutine(BossStun());
        }
    }
    IEnumerator BossStun()
    {
        anim.SetBool("Stunned", true);
        bossPhases[anim.GetInteger("Phase") - 1].SetActive(false);
        cooldown = false;
        fade.FadeInOutStatic(fadeTime);
        yield return new WaitForSeconds(fadeTime);
        turrets.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        fade.FadeInOutStatic(fadeTime);
        yield return new WaitForSeconds(fadeTime);
        anim.SetBool("Stunned", false);
        turrets.SetActive(false);
        bossPhases[anim.GetInteger("Phase") - 1].SetActive(true);
    }
}
