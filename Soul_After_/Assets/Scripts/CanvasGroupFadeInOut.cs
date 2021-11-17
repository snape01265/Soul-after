using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGroupFadeInOut : MonoBehaviour
{
    private CanvasGroup cg;
    public GameObject SceneTrans;
    private readonly float INTENSITY = .5f;

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public void CanvasGroupFadeInOther(CanvasGroup other)
    {
        StartCoroutine(FadeIn(other));
    }

    public void CanvasGroupFadeOutOther(CanvasGroup other)
    {
        StartCoroutine(FadeOut(other));
    }

    public void CanvasGroupFadeIn()
    {
        StartCoroutine(FadeIn(cg));
    }

    public void CanvasGroupFadeOut()
    {
        StartCoroutine(FadeOut(cg));
    }

    IEnumerator FadeIn(CanvasGroup cg)
    {
        while (1 - cg.alpha >= .01f)
        {
            cg.alpha += Time.deltaTime * INTENSITY;
            yield return null;
        }

        if (SceneTrans)
            SceneTrans.GetComponent<BoxCollider2D>().enabled = true;

        yield return null;
    }

    IEnumerator FadeOut(CanvasGroup cg)
    {
        if (SceneTrans)
            SceneTrans.GetComponent<BoxCollider2D>().enabled = false;

        while (1 - cg.alpha <= .99f)
        {
            cg.alpha -= Time.deltaTime * INTENSITY;
            yield return null;
        }
    }
}
