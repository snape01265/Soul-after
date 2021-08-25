using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryAcq : MonoBehaviour
{
    public int soul;
    public Vector2 force;
    private PlayerHealth playerHealth;
    private Rigidbody2D rb;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        /*force = new Vector2 (Random.Range(Random.Range(-2f,-1f), Random.Range(1f, 2f)), Random.Range(Random.Range(-2f, -1f), Random.Range(1f, 2f)));*/
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D rb)
    {
        if(rb.CompareTag("Player"))
        {
            playerHealth.AcquireSoul(soul);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector2 inDirection = rb.velocity;
        Vector2 inNormal = collision.contacts[0].normal;
        Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal);
        rb.AddForce(newVelocity, ForceMode2D.Impulse);
    }
}
