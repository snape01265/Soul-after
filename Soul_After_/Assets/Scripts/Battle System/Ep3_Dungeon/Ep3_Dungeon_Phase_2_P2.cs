using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ep3_Dungeon_Phase_2_P2 : MonoBehaviour
{
    public GameObject[] LavaFloors;
    public AudioSource sfx;

    private void OnEnable()
    {
        if (sfx)
            sfx.Play();
        StartCoroutine(RowOfFloors());
    }

    private void OnDisable()
    {
        foreach (GameObject g in LavaFloors)
            g.SetActive(false);
    }

    IEnumerator RowOfFloors()
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

            for (int i = 0; i< LavaFloors.Length; i++)
            {
                LavaFloors[i].GetComponent<RowFlame>().EnableRow();
                yield return new WaitForSeconds(1.6f);
                LavaFloors[i].GetComponent<RowFlame>().DisableRow();
            }
        }
    }
}
