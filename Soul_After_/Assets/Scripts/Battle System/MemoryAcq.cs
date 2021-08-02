using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryAcq : MonoBehaviour
{
    public int soul;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerHealth>())
        {
            other.GetComponent<PlayerHealth>().AcquireSoul(soul);
        }
    }
}
