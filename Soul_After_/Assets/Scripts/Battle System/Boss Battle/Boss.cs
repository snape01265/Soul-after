using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public float attackDuration;
    public Transform firePoint;
    public GameObject meteorPrefab;
    [HideInInspector]
    public Boss_Phase1 bossMovement;
    [HideInInspector]
    public GameObject turrets;
    [HideInInspector]
    public Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public void Stun()
    {
        StartCoroutine(BossStun());
    }
    public void FireMeteor()
    {
        Instantiate(meteorPrefab, firePoint.position, firePoint.rotation);
    }
    IEnumerator BossStun()
    {
        anim.SetBool("Stunned", true);
        //bossMovement.Pause();
        turrets.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        anim.SetBool("Stunned", false);
        //bossMovement.Resume();
        turrets.SetActive(false);
    }
}
