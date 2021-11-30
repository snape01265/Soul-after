using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DroppableBehavior : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int id;
    public UnityEvent callback;
    public bool NeedsFadeIn = true;
    private FlowerPuzzleBehavior FlowerPuzzle;
    private CanvasGroup objCanvas;
    private readonly float FADESPEED = .3f;

    private void Awake()
    {
        objCanvas = GetComponent<CanvasGroup>();
        FlowerPuzzle = GetComponentInParent<FlowerPuzzleBehavior>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DraggableBehavior>().id == id && FlowerPuzzle.curId == id)
        {
            callback.Invoke();
            if (NeedsFadeIn)
                StartCoroutine(Fadein());
            eventData.pointerDrag.GetComponent<DraggableBehavior>().isCorrect = true;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
        else eventData.pointerDrag.GetComponent<DraggableBehavior>().ResetPos();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DraggableBehavior>().id == id && FlowerPuzzle.curId == id)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f, 1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DraggableBehavior>().id == id && FlowerPuzzle.curId == id)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    IEnumerator Fadein()
    {
        while (objCanvas.alpha < .99f)
        {
            objCanvas.alpha += Time.deltaTime * FADESPEED;
            yield return null;
        }
    }
}
