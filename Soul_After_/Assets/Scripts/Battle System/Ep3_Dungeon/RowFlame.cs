using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowFlame : MonoBehaviour
{
    public GameObject[] Lavas;

    public void EnableRow()
    {
        StartCoroutine(RowOfFlames());
    }

    public void DisableRow()
    {
        foreach (GameObject g in Lavas)
        {
            g.GetComponent<FlameTrap>().Shrink();
        }
    }

    IEnumerator RowOfFlames()
    {
        for (int i = 0; i<Lavas.Length; i++)
        {
            Lavas[i].GetComponent<FlameTrap>().Expand();
            yield return new WaitForSeconds(.2f);
        }
    }
}
