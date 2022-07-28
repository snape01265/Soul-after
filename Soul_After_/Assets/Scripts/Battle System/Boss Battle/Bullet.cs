using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public int cooldownSeconds;
    [HideInInspector]
    public SpriteRenderer sprite;
    [HideInInspector]
    public Rigidbody2D rb;

    private List<Turret> turretList = new List<Turret>();
    [SerializeField]
    private Boss boss;
    private CameraControl cameraControl;

    void Start()
    {
        cameraControl = GameObject.Find("CMTurret").GetComponent<CameraControl>();
        //Below is previous scripts
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

        if (bossHP != null && boss.damaged)
        {
            bossHP.TakeDamage(damage);
            sprite.enabled = false;
            boss.anim.SetTrigger("Damage");
            StartCoroutine(DestroyBullet());
            cameraControl.ShakeCamera();
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
