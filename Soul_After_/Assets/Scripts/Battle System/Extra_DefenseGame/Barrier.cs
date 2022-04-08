using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public DefenseGameManager GameManager;
    [SerializeField]
    private int BarrierHealth;

    private void Start()
    {
        BarrierHealth = GameManager.BarrierHealth;
    }

    public void TakeDamage()
    {
        BarrierHealth -= GameManager.EnemyAtkDmg;

        if (BarrierHealth <= 0)
        {
            GameManager.OutForBlood = true;
            gameObject.SetActive(false);
        }
    }

    public void RestoreHealth()
    {
        BarrierHealth += GameManager.BarrierHealPerRound;
    }
}
