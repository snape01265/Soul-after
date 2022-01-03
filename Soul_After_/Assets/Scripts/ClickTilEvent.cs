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
    public AudioSource sfxPerClick;
    public AudioSource sfxAfterClick;

    private int number = 0;
    private bool done = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!done)
        {
            if (sfxPerClick != null)
                sfxPerClick.Play();
            number++;
            EventPerClick.Invoke();
            if (number > ClickNumber)
            {
                if (sfxAfterClick != null)
                    sfxAfterClick.Play();
                done = true;
                Callback.Invoke();
            }
        }
    }
}
