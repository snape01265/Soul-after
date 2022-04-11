using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrier : MonoBehaviour
{
    public DefenseGameManager GameManager;
    public Text BarrierText;
    [HideInInspector]
    public int BarrierHealth
    {
        get
        {
            return (barrierHealth);
        }
        set
        {
            barrierHealth = value;
            RenderBarrierHealth();
        }
    }
    private int barrierHealth;

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

    private void RenderBarrierHealth()
    {
        BarrierText.text = "º£¸®¾î: " + barrierHealth.ToString();
    }
}
