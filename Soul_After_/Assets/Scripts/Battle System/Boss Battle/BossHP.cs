using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    public int currentHealth;
    public HealthBar healthBar;
    [HideInInspector]
    public Animator anim;

    private int maxHealth = 100;
    void Start()
    {
        anim = this.GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 70 && 40 < currentHealth)
        {
            anim.SetInteger("Phase", 2);
        }
        else if (currentHealth <= 40 && 0 < currentHealth)
        {
            anim.SetInteger("Phase", 3);
        }
        else if (currentHealth <= 0)
        {
            //Death Animation?
            //Transition to Event Scene
        }
        else
        {
            anim.SetInteger("Phase", 1);
        }
    }
}
