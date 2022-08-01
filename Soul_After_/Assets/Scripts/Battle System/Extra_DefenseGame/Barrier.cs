using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrier : MonoBehaviour
{
    public DefenseGameManager GameManager;
    public Text BarrierText;
    public AudioSource TakeDmgSFX;
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
    private readonly int CLASSICBARRIERHEALTHLIMIT = 150;

    private void Start()
    {
        BarrierHealth = GameManager.BarrierHealth;
    }

    public void TakeDamage()
    {
        BarrierHealth -= GameManager.EnemyAtkDmg;
        TakeDmgSFX.Play();
        if (BarrierHealth <= 0)
        {
            GameManager.OutForBlood = true;
            gameObject.SetActive(false);
        }
    }

    public void RestoreHealth()
    {
        if (GameManager.isClassicMode)
            BarrierHealth = Mathf.Clamp(BarrierHealth + GameManager.BarrierHealPerRound, 0, CLASSICBARRIERHEALTHLIMIT);
        else
            BarrierHealth += GameManager.BarrierHealPerRound;
    }

    private void RenderBarrierHealth()
    {
        BarrierText.text = barrierHealth.ToString();
    }
}
