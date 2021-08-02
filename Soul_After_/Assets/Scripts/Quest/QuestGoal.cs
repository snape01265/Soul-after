using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public int goalValue;
    public int currentValue;
    public List<int> npcTalked = new List<int>();

    private ObjId objid;
    private bool exists;

    public bool obj0Talked = false;
    public bool obj1Talked = false;
    public bool obj2Talked = false;
    public bool obj3Talked = false;

    public bool IsReached()
    {
        if (goalType == GoalType.Talk && obj0Talked == true && obj1Talked == true && obj2Talked == true && obj3Talked == true)
        {
            return true;
        }
        else
        {
            return false;
        } 
    }
    public void NPC0Talked()
    {
        if (goalType == GoalType.Talk && obj0Talked == false)
        {
            obj0Talked = true;
        }
    }

    public void NPC1Talked()
    {
        if (goalType == GoalType.Talk && obj1Talked == false)
        {
            obj1Talked = true;
        }
    }
    public void NPC2Talked()
    {
        if (goalType == GoalType.Talk && obj2Talked == false)
        {
            obj2Talked = true;
        }
    }
    public void NPC3Talked()
    {
        if (goalType == GoalType.Talk && obj3Talked == false)
        {
            obj3Talked = true;
        }
    }
}

public enum GoalType
{
    Talk
}
