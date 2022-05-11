using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    private DefenseGameManager gameManager;
    private Rigidbody2D rb2D;
    private bool isMovable = false;
    private readonly float timer = 3f;

    private void Start()
    {
        gameManager = GameObject.Find("DefenseGameManager").GetComponent<DefenseGameManager>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isMovable)
        {
            isMovable = false;
            rb2D.velocity = transform.right * -gameManager.BulletSpd;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<DefenseMob>().TakeDamage();
            Destroy(gameObject);
        }
    }

    public void FireAtAngle(float Degrees)
    {
        transform.rotation = Quaternion.Euler(0, 0, Degrees);
        isMovable = true;
        StartCoroutine(SelfDestTimer());
    }

    IEnumerator SelfDestTimer()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);;
    }
}
