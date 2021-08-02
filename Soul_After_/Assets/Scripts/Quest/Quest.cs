using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
        Debug.Log("quest is completed!");
    }
}
