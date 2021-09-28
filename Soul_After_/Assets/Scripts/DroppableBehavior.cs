using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppableBehavior : MonoBehaviour, IDropHandler
{
    public int id;
    private FlowerPuzzleBehavior FlowerPuzzle;

    private void Awake()
    {
        FlowerPuzzle = GetComponentInParent<FlowerPuzzleBehavior>();    
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DraggableBehavior>().id == id && FlowerPuzzle.curId == id)
        {
            FlowerPuzzle.CheckFinished();
            eventData.pointerDrag.GetComponent<DraggableBehavior>().isCorrect = true;
            GetComponent<CanvasGroup>().alpha = 1f;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
        else eventData.pointerDrag.GetComponent<DraggableBehavior>().ResetPos();
    }
}
