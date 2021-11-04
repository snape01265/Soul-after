using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFlower : MonoBehaviour
{
    public int lane;
    public int flowerType;

    void Start()
    {
        GetComponent<Animator>().SetInteger("lane", lane);
        GetComponent<Animator>().SetInteger("flowerType", flowerType);
    }

    public void NextPhase()
    {
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    }
}
