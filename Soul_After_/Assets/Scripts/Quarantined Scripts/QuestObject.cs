using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    public int questNumber;
    public QuestHandler theQH;

    public void StartQuest()
    {

    }
    public void EndQuest()
    {
        theQH.questCompleted[questNumber] = true;
        gameObject.SetActive(false);
    }
}
