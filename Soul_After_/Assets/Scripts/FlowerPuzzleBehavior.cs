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
    public CanvasGroup Clouds;
    public CanvasGroup MarigoldDead;
    public CanvasGroup MarigoldAlive;
    public CanvasGroup BranchSnow;
    public CanvasGroup BranchCherry;
    public CanvasGroup Fog;
    public CanvasGroup BgSp;
    public CanvasGroup BgWt;

    public ParticleSystem SnowFall;
    public ParticleSystem RainFall;

    ParticleSystemController part;

    private void Awake()
    {
        part = GetComponent<ParticleSystemController>();
        fadeInOut = GetComponent<CanvasGroupFadeInOut>();
        curId = INITID;
    }

    public void CheckFinished()
    {
        curId += 1;
        switch (curId)
        {
            case 2:
                break;
            case 3:
                fadeInOut.CanvasGroupFadeInOther(Sun);
                fadeInOut.CanvasGroupFadeOutOther(Fog);
                fadeInOut.CanvasGroupFadeOutOther(Clouds);
                part.ParticleSystemFadeOut(SnowFall, 0, 4);
                // 이하 dialogue (word 4, 5) 이후 일어나야 할 일들
                part.ParticleSystemFadeIn(RainFall, 10, 4);
                break;

            case 4:
                fadeInOut.CanvasGroupFadeInOther(BgSp);
                fadeInOut.CanvasGroupFadeOutOther(BgWt);
                fadeInOut.CanvasGroupFadeOutOther(BranchSnow);
                fadeInOut.CanvasGroupFadeInOther(BranchCherry);
                fadeInOut.CanvasGroupFadeOutOther(MarigoldDead);
                fadeInOut.CanvasGroupFadeInOther(MarigoldAlive);
                StartCoroutine(WaitForEnd(5));
                break;
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

    IEnumerator WaitForEnd(float t)
    {
        yield return new WaitForSeconds(t);
        Debug.Log("Puzzle Finished!");
    }
}
