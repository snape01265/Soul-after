using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerPuzzleBehavior : MonoBehaviour
{
    private readonly int INITID = 1;
    private readonly int FINID = 4;
    private CanvasGroupFadeInOut fadeInOut;

    [HideInInspector]
    public int curId;

    public CanvasGroup Sun;
    public CanvasGroup MarigoldDead;
    public CanvasGroup MarigoldAlive;
    public CanvasGroup BranchSnow;
    public CanvasGroup BranchCherry;
    public CanvasGroup Fog;
    public CanvasGroup BgSp;
    public CanvasGroup BgWt;

    private void Awake()
    {

        fadeInOut = GetComponent<CanvasGroupFadeInOut>();
        curId = INITID;
    }

    public void CheckFinished()
    {
        if (curId == FINID)
        {
            curId += 1;
            Debug.Log("Puzzle Finished!");
        }
        else 
        {
            curId += 1;
            switch (curId)
            {
                case 2:
                    break;
                case 3:
                    fadeInOut.CanvasGroupFadeInOther(Sun);
                    break;
                case 4:
                    fadeInOut.CanvasGroupFadeOutOther(Fog);
                    fadeInOut.CanvasGroupFadeOutOther(MarigoldDead);
                    fadeInOut.CanvasGroupFadeInOther(MarigoldAlive);
                    fadeInOut.CanvasGroupFadeInOther(BgSp);
                    fadeInOut.CanvasGroupFadeOutOther(BgWt);
                    fadeInOut.CanvasGroupFadeOutOther(BranchSnow);
                    fadeInOut.CanvasGroupFadeInOther(BranchCherry);
                    break;
            }
        }
    }

    public void CheckSlider()
    {
        if (curId == FINID)
        {
            CheckFinished();
        } else if (curId == FINID + 1)
        {
            GetComponentInChildren<Slider>().value = 1;
        } else
        {
            GetComponentInChildren<Slider>().value = 0;
        }
    }
}
