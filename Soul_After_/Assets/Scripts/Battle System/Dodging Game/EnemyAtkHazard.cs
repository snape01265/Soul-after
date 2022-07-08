using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkHazard : MonoBehaviour
{
    public int damage;
    private PlayerHealth health;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>() && !other.GetComponent<PlayerHealth>().PainState)
        {
            health = other.GetComponent<PlayerHealth>();
            health.TakeDamage(damage);
        }
    }
}