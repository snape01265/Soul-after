using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public float attackDuration;
    public Transform firePoint;
    public GameObject meteorPrefab;
    //[HideInInspector]
    public Boss_Phase1 bossMovement;
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
        anim.SetBool("Stunned", true);
        bossMovement.enabled = false;
        turrets.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        anim.SetBool("Stunned", false);
        bossMovement.enabled = true;
        turrets.SetActive(false);
        cooldown = false;
    }
}
