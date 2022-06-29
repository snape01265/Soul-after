using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class EndPuzzleFunction : MonoBehaviour
{
    public GameObject Player_Sub;

    public void Conditional_Activation()
    {
        bool InvisibleMaze_Finished = DialogueLua.GetVariable("Ep_5_Puzzle.Memory_1").asBool;
        bool ChaseMaze_Finished = DialogueLua.GetVariable("Ep_5_Puzzle.Memory_2").asBool;
        bool SimonSays_Finished = DialogueLua.GetVariable("Ep_5_Puzzle.Memory_3").asBool;

        if (InvisibleMaze_Finished && ChaseMaze_Finished && SimonSays_Finished)
        {
            Player_Sub.SetActive(true);
        }
        else
        {
            Player_Sub.SetActive(false);
        }
    }
}
