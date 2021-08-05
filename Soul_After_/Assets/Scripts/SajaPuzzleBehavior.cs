using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SajaPuzzleBehavior : MonoBehaviour
{
    public List<GameObject> buttons;
    public BoolList pressedStates;
    private readonly List<bool> FinishedState = new List<bool>(6) { true, true, true, true, true, true };
    private bool finished = false;

    private void Awake()
    {
        Debug.Log(this.name + " is awake!");
    }

    void Update()
    {
        if (!finished && FinishedState.Equals(pressedStates.initialValue))
        {
            // finish event
            Debug.Log("Event Finished!");
            finished = true;
        }
    }
}
