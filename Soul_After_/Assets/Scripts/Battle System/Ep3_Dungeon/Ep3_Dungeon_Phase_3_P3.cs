using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ep3_Dungeon_Phase_3_P3 : MonoBehaviour
{
    public Flameshot[] Flames;
    public AudioSource sfx;
    public float Duration;

    private Animator bossAnim;

    private void OnEnable()
    {
        bossAnim = GetComponent<Animator>();
        if (sfx)
            sfx.Play();
        StartCoroutine(ColOfFloors());
    }

    private void OnDisable()
    {
        foreach (Flameshot g in Flames)
            g.gameObject.SetActive(false);
    }

    IEnumerator ColOfFloors()
    {
        bool first = true;
        int idx = 0;
        while (true)
        {
            if (first)
            {
                first = false;
                if (bossAnim != null)
                    bossAnim.SetTrigger("Attack");
            }
            else
            {
                yield return new WaitForSeconds(Duration);

                if (sfx)
                    sfx.Play();

                if (bossAnim != null)
                    bossAnim.SetTrigger("Attack");
            }

            for (int j = 0; j < Flames.Length; j++)
            {
                if (j != idx)
                    Flames[j].FireFlames();
            }

            idx = idx == 0 ? Flames.Length - 1 : 0;
        }
    }
}
