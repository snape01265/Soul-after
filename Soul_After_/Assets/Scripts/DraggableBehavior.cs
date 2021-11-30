using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBehavior : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    private FlowerPuzzleBehavior FlowerPuzzle;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 initRect;
    private CanvasGroupFadeInOut fadeInOut;
    private bool draggable = false;

    [HideInInspector] public bool isCorrect = false;
    public int id;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initRect = rectTransform.anchoredPosition;
        fadeInOut = GetComponent<CanvasGroupFadeInOut>();
        FlowerPuzzle = GetComponentInParent<FlowerPuzzleBehavior>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            canvasGroup.alpha = 0.7f;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            if (isCorrect)
            {
                fadeInOut.CanvasGroupFadeOut();
            }
            else
            {
                ResetPos();
                canvasGroup.blocksRaycasts = true;
                fadeInOut.CanvasGroupFadeIn();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (id == FlowerPuzzle.curId)
            draggable = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggable)
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void ResetPos()
    {
        Vector2 startPos = rectTransform.anchoredPosition;
        StartCoroutine(SmoothMove(startPos));
    }

    public IEnumerator SmoothMove(Vector2 startPos)
    {
        float currentTime = 0;
        float endTime = .3f;
        float normalizedValue;

        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, initRect, Time.deltaTime * .01f);

        while (currentTime <= endTime)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / endTime;

            rectTransform.anchoredPosition = Vector2.Lerp(startPos, initRect, normalizedValue);
            yield return null;
        }

        draggable = false;
    }
}
