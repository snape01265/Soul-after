using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public float attackDuration;
    public Transform firePoint;
    public GameObject meteorPrefab;
    public Boss_Phase1 phase1;
    public Boss_Phase2 phase2;
    public Boss_Phase3 phase3;
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
    public void FireMeteor()
    {
        Instantiate(meteorPrefab, firePoint.position, firePoint.rotation);
    }
    IEnumerator BossStun()
    {
        if (anim.GetInteger("Phase") == 1)
        {
            anim.SetBool("Stunned", true);
            phase1.enabled = false;
            turrets.SetActive(true);
            yield return new WaitForSeconds(attackDuration);
            anim.SetBool("Stunned", false);
            phase1.enabled = true;
            turrets.SetActive(false);
            cooldown = false;
        }
        else if (anim.GetInteger("Phase") == 2)
        {
            anim.SetBool("Stunned", true);
            phase2.enabled = false;
            turrets.SetActive(true);
            yield return new WaitForSeconds(attackDuration);
            anim.SetBool("Stunned", false);
            phase2.enabled = true;
            turrets.SetActive(false);
            cooldown = false;
        }
        else if (anim.GetInteger("Phase") == 3)
        {
            anim.SetBool("Stunned", true);
            phase3.enabled = false;
            turrets.SetActive(true);
            yield return new WaitForSeconds(attackDuration);
            anim.SetBool("Stunned", false);
            phase3.enabled = true;
            turrets.SetActive(false);
            cooldown = false;
        }
    }
}
