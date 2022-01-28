using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ep3_Dungeon_Phase_3_P1 : MonoBehaviour
{
    public GameObject[] LavaFloors;
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
        foreach (GameObject g in LavaFloors)
            g.SetActive(false);
    }

    IEnumerator ColOfFloors()
    {
        bool first = true;

        while (true)
        {
            if (first)
                first = !first;
            else
            {
                yield return new WaitForSeconds(1.5f);

                if (sfx)
                    sfx.Play();
            }

            for (int i = LavaFloors.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < LavaFloors.Length; j++)
                {
                    if (j != i)
                        LavaFloors[j].GetComponent<RowFlame>().EnableRow();
                }

                yield return new WaitForSeconds(Duration);

                for (int j = 0; j < LavaFloors.Length; j++)
                    LavaFloors[j].GetComponent<RowFlame>().DisableRow();        
            }
        }
    }
}