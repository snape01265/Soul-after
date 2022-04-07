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
    [SerializeField]
    private bool inPosition = false;
    private Vector3 originPos;
    private Vector3 targetPos;

    void Start()
    {
        tag = "Enemy";
        Player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.Find("DefenseGameManager").GetComponent<DefenseGameManager>();
        barrier = GameObject.Find("Barrier").GetComponent<Barrier>();
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
            }
            transform.position = Vector3.Lerp(transform.position, Player.transform.position, normSpeed);
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
