using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPosition : MonoBehaviour
{
    public VectorValue position;
    public Vector2 changedPosition;
    public Vector2 sceneChangePosition;

    void Start()
    {
        transform.position = position.initialValue;
    }
    public void SetPosition()
    {
        position.initialValue = changedPosition;
    }
    public void ChangePosition()
    {
        changedPosition = sceneChangePosition;
    }
}
