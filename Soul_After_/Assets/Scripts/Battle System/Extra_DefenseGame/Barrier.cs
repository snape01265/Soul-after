using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public DefenseGameManager GameManager;
    public int BarrierHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<DefenseMob>().AttemptAtk();
        }
    }

    public void TakeDamage()
    {
        BarrierHealth -= GameManager.EnemyAtkDmg;

        if (BarrierHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void RestoreHealth()
    {
        BarrierHealth += GameManager.BarrierHealPerRound;
    }
}
