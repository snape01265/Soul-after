using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class SignalReader : MonoBehaviour
{
    public Signal contextOn;
    public Signal contextOff;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            contextOn.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            contextOff.Raise();
        }
    }
}
