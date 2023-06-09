using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseMob : MonoBehaviour
{
    public AudioSource HitSFX;
    public AudioClip DeathSFX;

    private GameObject Player;
    private DefenseGameManager gameManager;
    private Animator anim;
    private int health;
    private int gunDmg;
    private float normSpeed;
    private Barrier barrier;
    private bool inPosition = false;
    private bool isDying = false;
    private Vector3 originPos;
    private Vector3 targetPos;

    void Start()
    {
        tag = "Enemy";
        Player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.Find("DefenseGameManager").GetComponent<DefenseGameManager>();
        if (!gameManager.OutForBlood)
            barrier = GameObject.Find("Barrier").GetComponent<Barrier>();
        anim = GetComponent<Animator>();
        originPos = transform.position;
        health = gameManager.EnemyHealth;
        gunDmg = gameManager.GunAtkDmg;
        targetPos = new Vector3(barrier.transform.position.x, originPos.y, originPos.z);
    }

    void FixedUpdate()
    {
        normSpeed += Time.deltaTime * gameManager.EnemySpd;
        if (gameManager.OutForBlood)
        {
            if (AtkMotion() != null)
            {
                StopCoroutine(AtkMotion());
                anim.SetBool("Attack", false);
            }
            transform.position = Vector3.Lerp(transform.position, Player.transform.position, 0.05f);
        }
        else if (!inPosition)
            transform.position = Vector3.Lerp(originPos, targetPos, normSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.EndGame();
        } else if (collision.CompareTag("Barrier"))
        {
            anim.SetBool("Attack", true);
            AttemptAtk();
        }
    }

    public void AttemptAtk()
    {
        inPosition = true;
        StartCoroutine(AtkMotion());
    }

    public void TakeDamage()
    {
        HitSFX.Play();
        health -= gunDmg;

        if (!isDying && health <= 0)
        {
            isDying = true;
            GetComponent<BoxCollider2D>().enabled = false;
            gameManager.WaveKill += 1;
            gameManager.CurScore += 1;
            StopAllCoroutines();
            StartCoroutine(DeathMotion());
        }
    }

    IEnumerator AtkMotion()
    {
        yield return new WaitForSeconds(gameManager.EnemyAtkTime);
        barrier.TakeDamage();
        StartCoroutine(AtkMotion());
    }

    IEnumerator DeathMotion()
    {
        HitSFX.PlayOneShot(DeathSFX);
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }
}
