using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ep3_Dungeon_Phase_2_P2 : MonoBehaviour
{
    public GameObject[] LavaFloors;

    private void OnEnable()
    {
        StartCoroutine(RowOfFloors());
    }

    private void OnDisable()
    {
        foreach (GameObject g in LavaFloors)
            g.SetActive(false);
    }

    IEnumerator RowOfFloors()
    {
        for (int i = 0; i < LavaFloors.Length; i++)
        {
            LavaFloors[i].GetComponent<RowFlame>().EnableRow();
            yield return new WaitForSeconds(4f);
            LavaFloors[i].GetComponent<RowFlame>().DisableRow();
        }
    }
}
