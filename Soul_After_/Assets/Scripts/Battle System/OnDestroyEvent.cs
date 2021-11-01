using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyEvent : MonoBehaviour
{
    public int damage;
    private CatchController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CatchController>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        player.GetFlower(damage);
    }
    private void OnDestroy()
    {
        player.TakeDamage(damage);
    }
}
