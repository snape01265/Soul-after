using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingMemoryBehavior : MonoBehaviour
{
    public BoolList mazeState;

    public void EnableMazeState(int idx)
    {
        if (!mazeState.initialValue[idx])
            mazeState.initialValue[idx] = true;
    }
}