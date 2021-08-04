using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SajaPuzzleBehavior : MonoBehaviour
{
    public List<GameObject> Buttons;
    public BoolList PressedStates;
    private readonly List<bool> finishedState = new List<bool> { true, true, true, true, true, true };

    private void Update()
    {
        if (finishedState.Equals(PressedStates.initialValue)) {
            // finish event
        }
    }
}
