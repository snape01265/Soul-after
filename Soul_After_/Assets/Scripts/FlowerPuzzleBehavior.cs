using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerPuzzleBehavior : MonoBehaviour
{
    private readonly int initId = 1;
    private readonly int finId = 5;
    [HideInInspector] public int curId;

    private void Awake()
    {
        curId = initId;
    }

    public void CheckFinished()
    {
        if (curId == finId)
        {
            curId += 1;
            Debug.Log("Puzzle Finished!");
        }
        else curId += 1;
    }

    public void CheckSlider()
    {
        if (curId == finId)
        {
            CheckFinished();
        } else if (curId == finId + 1)
        {
            GetComponentInChildren<Slider>().value = 1;
        } else
        {
            GetComponentInChildren<Slider>().value = 0;
        }
    }
}
