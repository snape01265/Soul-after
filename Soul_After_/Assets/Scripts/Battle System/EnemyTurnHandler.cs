using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnHandler : MonoBehaviour
{
    public bool FinishedTurn;
    public int attackType;

    void Start()
    {
        FinishedTurn = false;
        attackType = GameObject.FindGameObjectWithTag("GameController").GetComponent<TurnHandler>().phaseCount;
        GetComponent<Animator>().SetInteger("AtkType", attackType);
    }

    public void AtkDone()
    {
        FinishedTurn = true;
    }
}
