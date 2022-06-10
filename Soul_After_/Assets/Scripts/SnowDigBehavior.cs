using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SnowDigBehavior : MonoBehaviour, IDragHandler
{
    public int id;
    public AudioSource sfx;
    public CanvasGroupFadeInOut Ground;

    private FlowerPuzzleBehavior FlowerPuzzle;
    private CanvasGroup snow;
    private bool done = false;
    private readonly float DRAGFORCE = .00001f;

    private void Awake()
    {
        FlowerPuzzle = GetComponentInParent<FlowerPuzzleBehavior>();
        snow = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!done && FlowerPuzzle.curId == id)
        {
            if (snow.alpha > .1f)
                snow.alpha -= eventData.delta.sqrMagnitude * DRAGFORCE;
            else
            {
                done = true;
                if (Ground)
                    Ground.CanvasGroupFadeIn();
                FlowerPuzzle.CheckFinished();
                GetComponent<Image>().raycastTarget = false;
                if (sfx != null)
                    sfx.Play();
            }
        }
    }
}
