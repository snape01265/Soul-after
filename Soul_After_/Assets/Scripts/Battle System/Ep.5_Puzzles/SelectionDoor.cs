using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionDoor : MonoBehaviour
{
    public SelectionDoorManager DoorManager;
    public SelectionDoorManager.DOORTYPE DoorType;

    public void FinishedPuzzle()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (DoorType)
            {
                case SelectionDoorManager.DOORTYPE.InvisPuzzle:
                    DoorManager.InvisPuzzle.InitPuzzle();
                    break;
                case SelectionDoorManager.DOORTYPE.ChasePuzzle:
                    DoorManager.ChasePuzzle.InitPuzzle();
                    break;
                case SelectionDoorManager.DOORTYPE.SimonPuzzle:
                    DoorManager.SimonPuzzle.InitPuzzle();
                    break;
            }
        }
    }
}
