using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnHandler : MonoBehaviour
{
    public bool FinishedTurn;
    public int level;
    public int attackType;

    void Start()
    {
        FinishedTurn = false;
        level = GameObject.FindGameObjectWithTag("GameController").GetComponent<TurnHandler>().phaseCount;
        GetComponent<Animator>().SetInteger("Level", level);
        GetComponent<Animator>().SetInteger("AtkType", attackType);
    }

    public void AtkDone()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<TurnHandler>().patternCount += 1;
        FinishedTurn = true;
    }
    public void NextPhase()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<TurnHandler>().PatternFinished();
    }
}
