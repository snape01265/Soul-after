using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionDoorManager : MonoBehaviour
{
    public SelectionDoor[] Doors;
    public BoolList Progress;
    public InvismazePuzzleManager InvisPuzzle;
    public ChasemazePuzzleManager ChasePuzzle;
    public SimonPuzzleManager SimonPuzzle;

    public enum DOORTYPE
    {
        InvisPuzzle,
        ChasePuzzle,
        SimonPuzzle
    }

    private void Start()
    {
        TrackProgress();
    }

    public void TrackProgress()
    {
        foreach (var isFin in Progress.initialValue.Select((value, index) => (value, index)))
        {
            if (isFin.value)
            {
                Doors[isFin.index].FinishedPuzzle();
            }
        }
    }
}
