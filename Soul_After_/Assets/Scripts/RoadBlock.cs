using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    public BoolValue trigger;
    public BoxCollider2D physicsCollider;
    void Update()
    {
        if(trigger.initialValue)
        {
            Open();
        }
    }

    public void Open()
    {
        gameObject.SetActive(!trigger.initialValue);
    }

    public void QuestTrigger()
    {
        trigger.initialValue = true;
    }
}
