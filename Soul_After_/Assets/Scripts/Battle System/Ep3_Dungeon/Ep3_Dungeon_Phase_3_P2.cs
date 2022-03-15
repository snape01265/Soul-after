using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ep3_Dungeon_Phase_3_P2 : MonoBehaviour
{
    public UnityEvent ActionPerTime;
	public float ActionTime;
    public AudioSource sfx;

    private Animator bossAnim;

    private void OnEnable()
    {
        bossAnim = GetComponent<Animator>();
        StartCoroutine(FirePattern());
    }

    IEnumerator FirePattern()
    {
        while (true)
        {
            if (bossAnim != null)
                bossAnim.SetTrigger("Attack");

            if (sfx)
                sfx.Play();
            ActionPerTime.Invoke();
            yield return new WaitForSeconds(ActionTime);
        }
    }
}
