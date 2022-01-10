using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

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

    public Animation FlowerAnim;
    //Flower and dirt Animator for Marigold Alive
    public Animator MarigoldAnim;
    public Animator DirtAnim;

    public DialogueSystemTrigger Hint2;
    public DialogueSystemTrigger Hint3;
    public DialogueSystemTrigger Hint4;
    public DialogueSystemTrigger Hint5;

    public ParticleSystem SnowFall;
    public ParticleSystem RainFall;

    //Audiosource
    public AudioSource RainSFX;

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
                if (FlowerAnim)
                    FlowerAnim.Play();

                MarigoldDead.blocksRaycasts = false;
                // 이후 animator로 대체
                fadeInOut.CanvasGroupFadeOutOther(MarigoldAlive);
                fadeInOut.CanvasGroupFadeOutOther(MarigoldDead);
                Hint2.OnUse();
                break;
            case 3:
                Hint3.OnUse();
                break;
            case 4:
                fadeInOut.CanvasGroupFadeOutOther(Hole);
                fadeInOut.CanvasGroupFadeInOther(Sun);
                fadeInOut.CanvasGroupFadeOutOther(Fog);
                Hint4.OnUse();
                break;
            case 5:
                part.ParticleSystemFadeOut(SnowFall, 0, 6);
                Hint5.OnUse();
                break;
            case 6:
                fadeInOut.CanvasGroupFadeInOther(BgSp);
                fadeInOut.CanvasGroupFadeOutOther(BgWt);
                fadeInOut.CanvasGroupFadeOutOther(BranchSnow);
                fadeInOut.CanvasGroupFadeInOther(BranchCherry);
                fadeInOut.CanvasGroupFadeInOther(MarigoldAlive);
                MarigoldAnim.SetTrigger("Grow");
                DirtAnim.SetTrigger("Change");
                RainFall.Stop();
                RainSFX.Stop();
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

    //Rain particle stop method
    public void RainFadeOut()
    {
        part.ParticleSystemFadeIn(RainFall, 10, 6);
    }
}
