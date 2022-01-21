using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ep3_Dungeon_Phase_3_P3 : MonoBehaviour
{
    public Flameshot[] Flames;
    public AudioSource sfx;
    public float Duration;

    private void OnEnable()
    {
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
                first = !first;
            }
            else
            {
                yield return new WaitForSeconds(Duration);

                if (sfx)
                    sfx.Play();
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
