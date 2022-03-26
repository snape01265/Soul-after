using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public int cooldownSeconds;
    public AudioSource sfx;
    [HideInInspector]
    public SpriteRenderer sprite;
    [HideInInspector]
    public Rigidbody2D rb;

    private List<Turret> turretList = new List<Turret>();
    private Boss boss;

    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>();
        rb.velocity = transform.right * speed;
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach(GameObject turret in turrets)
        {
            Turret t = turret.GetComponent<Turret>();
            turretList.Add(t);
        }
        StartCoroutine(BulletCD(cooldownSeconds));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BossHP bossHP = other.GetComponent<BossHP>();
        if (bossHP != null)
        {
            if (sfx)
                sfx.Play();
            bossHP.TakeDamage(damage);
            sprite.enabled = false;
            boss.anim.SetTrigger("Damage");
            StartCoroutine(DestroyBullet());
        }
        else
        {
            StartCoroutine(DestroyBullet());
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    IEnumerator BulletCD(int seconds)
    {
        foreach(Turret t in turretList)
        {
            t.cooldown = true;
        }
        yield return new WaitForSeconds(seconds);
        foreach (Turret t in turretList)
        {
            t.cooldown = false;
        }
    }
}
