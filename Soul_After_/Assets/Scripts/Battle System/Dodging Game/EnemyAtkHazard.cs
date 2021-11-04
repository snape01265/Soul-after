using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkHazard : MonoBehaviour
{
    public int damage;
    private bool damaged;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerHealth>() && !damaged)
        {
            damaged = true;
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            StartCoroutine(WaitForDmg());
        }
    }

    private IEnumerator WaitForDmg()
    {
        yield return new WaitForSeconds(.5f);
        damaged = false;
    }
}
