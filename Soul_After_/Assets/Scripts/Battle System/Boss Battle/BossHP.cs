using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossHP : MonoBehaviour
{
    public int currentHealth;
    public HealthBar healthBar;
    public PlayableDirector endScene;
    public AudioSource sfx;
    public GameObject turret;

    private GameObject boss;
    private int maxHealth = 150;
    private Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        boss = GameObject.FindGameObjectWithTag("Enemy");
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        Boss bossScript = boss.GetComponent<Boss>();

        if (sfx)
            sfx.Play();
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth == maxHealth * 2/3)
        {
            anim.SetInteger("Phase", 2);
            bossScript.phaseChange = true;
        }
        else if (currentHealth == maxHealth * 1/3)
        {
            anim.SetInteger("Phase", 3);
            bossScript.phaseChange = true;
        }
        else if (currentHealth <= 0)
        {
            //Death Animation?
            turret.SetActive(false);
            endScene.Play();
        }
    }
}
