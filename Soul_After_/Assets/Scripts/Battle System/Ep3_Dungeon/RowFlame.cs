using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowFlame : MonoBehaviour
{
    public GameObject[] Lavas;
    public float Speed;

    public void EnableRow()
    {
        StartCoroutine(RowOfFlames());
    }

    public void DisableRow()
    {
        foreach (GameObject g in Lavas)
        {
            g.GetComponent<CircleCollider2D>().enabled = false;
            g.GetComponent<FlameTrap>().Shrink();
        }
    }

    IEnumerator RowOfFlames()
    {
        for (int i = 0; i<Lavas.Length; i++)
        {
            Lavas[i].GetComponent<CircleCollider2D>().enabled = true;
            Lavas[i].GetComponent<FlameTrap>().Expand();
            yield return new WaitForSeconds(Speed);
        }
    }
}
