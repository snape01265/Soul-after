using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerPuzzleBehavior : MonoBehaviour
{
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
    public List<CanvasGroup> Seeds;
    public CanvasGroup Hole;

    public ParticleSystem SnowFall;
    public ParticleSystem RainFall;

    private ParticleSystemController part;
    private CanvasGroupFadeInOut fadeInOut;

    private readonly int INITID = 1;

    private int seedNo = 0;
    private int plantNo = 0;

    private void Awake()
    {
        part = GetComponent<ParticleSystemController>();
        fadeInOut = GetComponent<CanvasGroupFadeInOut>();
        curId = INITID;
    }

    public void CheckFinished()
    {
        curId++;
        switch (curId)
        {
            case 2:
                // 이후 animator로 대체
                MarigoldAlive.blocksRaycasts = false;
                fadeInOut.CanvasGroupFadeOutOther(MarigoldAlive);
                fadeInOut.CanvasGroupFadeOutOther(MarigoldDead);
                break;
            case 3:
                break;
            case 4:
                fadeInOut.CanvasGroupFadeOutOther(Hole);
                fadeInOut.CanvasGroupFadeInOther(Sun);
                fadeInOut.CanvasGroupFadeOutOther(Fog);
                fadeInOut.CanvasGroupFadeOutOther(Clouds);
                part.ParticleSystemFadeOut(SnowFall, 0, 4);
                // 이하 dialogue (word 4, 5) 이후 일어나야 할 일들
                part.ParticleSystemFadeIn(RainFall, 10, 4);
                break;
            case 5:
                fadeInOut.CanvasGroupFadeInOther(BgSp);
                fadeInOut.CanvasGroupFadeOutOther(BgWt);
                fadeInOut.CanvasGroupFadeOutOther(BranchSnow);
                fadeInOut.CanvasGroupFadeInOther(BranchCherry);
                fadeInOut.CanvasGroupFadeInOther(MarigoldAlive);
                StartCoroutine(WaitForEnd(5));
                break;
        }
    }

    public void ShowSeeds()
    {
        if (seedNo < 3)
            fadeInOut.CanvasGroupFadeInOther(Seeds[seedNo]);
        seedNo++;
        if (seedNo == 3)
            CheckFinished();
    }

    public void PlantSeeds()
    {
        plantNo++;
        if (plantNo == 3)
            CheckFinished();
    }

    IEnumerator WaitForEnd(float t)
    {
        yield return new WaitForSeconds(t);
        Debug.Log("Puzzle Finished!");
    }
}
