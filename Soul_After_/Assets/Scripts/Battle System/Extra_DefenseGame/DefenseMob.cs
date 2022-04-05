using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseMob : MonoBehaviour
{
    private GameObject Player;
    private DefenseGameManager gameManager;
    private int health;
    private int gunDmg;
    private float normSpeed;
    private Barrier barrier;
    private bool inPosition = false;
    private Vector3 originPos;

    void Start()
    {
        tag = "Enemy";
        Player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.Find("DefenseGameManager").GetComponent<DefenseGameManager>();
        barrier = GameObject.Find("Barrier").GetComponent<Barrier>();
        originPos = transform.localPosition;
        health = gameManager.EnemyHealth;
        gunDmg = gameManager.GunAtkDmg;
    }

    void FixedUpdate()
    {
        normSpeed += Time.deltaTime * gameManager.EnemySpd;
        if (gameManager.OutForBlood)
        {
            if (AtkMotion() != null)
            {
                StopCoroutine(AtkMotion());
            }
            transform.localPosition = Vector3.Lerp(originPos, Player.transform.position, normSpeed);
        }
        else if (!inPosition)
            transform.localPosition = Vector3.Lerp(originPos, barrier.transform.position, normSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.EndGame();
        }
    }

    public void AttemptAtk()
    {
        inPosition = true;
        StartCoroutine(AtkMotion());
    }

    public void TakeDamage()
    {
        health -= gunDmg;

        if (health <= 0)
        {
            gameManager.WaveKill += 1;
            gameManager.CurScore += 1;
            Destroy(gameObject);
        }
    }

    IEnumerator AtkMotion()
    {
        // some attackmotion or whatever
        yield return new WaitForSeconds(gameManager.EnemyAtkTime);
        barrier.TakeDamage();
        StartCoroutine(AtkMotion());
    }
}
