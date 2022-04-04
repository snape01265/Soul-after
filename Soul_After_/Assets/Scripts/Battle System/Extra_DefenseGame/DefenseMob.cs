using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseMob : MonoBehaviour
{
    public float speed;

    private DefenseGameManager gameManager;
    private Barrier barrier;
    private bool inPosition = false;

    void Start()
    {
        tag = "Enemy";
        gameManager = GameObject.Find("DefenseGameManager").GetComponent<DefenseGameManager>();
        barrier = GameObject.Find("Barrier").GetComponent<Barrier>();
    }

    void FixedUpdate()
    {
        if (!inPosition)
            transform.localPosition += Vector3.right * speed;
    }

    public void AttemptAtk()
    {
        inPosition = true;
        StartCoroutine(AtkMotion());

        IEnumerator AtkMotion()
        {
            // some attackmotion or whatever
            yield return new WaitForSeconds(gameManager.EnemyAtkTime);
            barrier.TakeDamage();
            yield return StartCoroutine(AtkMotion());
        }
    }
}
