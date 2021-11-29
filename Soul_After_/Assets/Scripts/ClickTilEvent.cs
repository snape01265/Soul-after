using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickTilEvent : MonoBehaviour, IPointerDownHandler
{
    public int ClickNumber;
    public UnityEvent EventPerClick;
    public UnityEvent Callback;
    private int number = 0;
    private bool done = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(number);
        number++;
        EventPerClick.Invoke();
        if (!done && number > ClickNumber)
        {
            done = true;
            Callback.Invoke();
        }
    }
}
