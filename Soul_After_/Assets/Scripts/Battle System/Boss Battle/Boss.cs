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

    void Start()
    {
        anim = this.GetComponent<Animator>();
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
        turrets.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        anim.SetBool("Stunned", false);
        turrets.SetActive(false);
        bossPhases[anim.GetInteger("Phase") - 1].SetActive(true);
        cooldown = false;
    }
}
