using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public float stunDuration;
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
    IEnumerator BossStun()
    {
        anim.SetBool("Stunned", true);
        //bossMovement.Pause();
        turrets.SetActive(true);
        yield return new WaitForSeconds(stunDuration);
        anim.SetBool("Stunned", false);
        //bossMovement.Resume();
        turrets.SetActive(false);
    }
}
