using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public QuestGoal goal;

    public Player player;

    void Start()
    {

    }
    public void AcceptQuestTalk()
    {
        quest.isActive = true;
        player.quest = quest;
        goal.goalType = GoalType.Talk;
    }
}
