using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBehavior : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 initRect;
    private CanvasGroupFadeInOut fadeInOut;

    [HideInInspector] public bool isCorrect = false;
    public int id;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initRect = rectTransform.anchoredPosition;
        fadeInOut = GetComponent<CanvasGroupFadeInOut>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        fadeInOut.CanvasGroupFadeOut();
        canvasGroup.blocksRaycasts = true;
        if(!isCorrect)
        {
            fadeInOut.CanvasGroupFadeIn();
            ResetPos();
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down!");
    }

    public void OnDrag(PointerEventData eventData)
    {
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
    }
}
