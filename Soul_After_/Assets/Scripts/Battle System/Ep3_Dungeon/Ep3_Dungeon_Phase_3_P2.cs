using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ep3_Dungeon_Phase_3_P2 : MonoBehaviour
{
    public UnityEvent ActionPerTime;
	public float ActionTime;
    public AudioSource sfx;

    private void Start()
    {
        StartCoroutine(FirePattern());
    }

    IEnumerator FirePattern()
    {
        while (true)
        {
            if (sfx)
                sfx.Play();
            ActionPerTime.Invoke();
            yield return new WaitForSeconds(ActionTime);
        }
    }
}
