using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceBehavior : MonoBehaviour
{
    private readonly float PLAYERSPEED = 2f;
    private readonly float BUFFER = 5f;
    private readonly float BONUS = .12f;
    private readonly float NORM = .08f;
    private readonly float PENALTY = .04f;
    private readonly float WINCOND = 100f;

    public Transform player;
    public Transform enemy;
    public Transform startObj;
    public Transform endObj;

    private float startPos;
    private float endPos;

    private float playerTotal;
    private float AITotal;
    
    private bool raceOver;

    private void Awake()
    {
        startPos = startObj.position.x;
        endPos = endObj.position.x;

        playerTotal = 0;
        AITotal = 0;
    }

    private void Update()
    {
        if (!raceOver)
        {
            if (Input.GetButtonDown("Jump"))
                PlayerSwims();
            AISwims();
            RenderSwimmers();
            CheckRaceFinish();
        }
    }

    private void CheckRaceFinish()
    {
        if (playerTotal > WINCOND)
            PlayerWins();
        else if (AITotal > WINCOND)
            PlayerLoses();
    }

    private void AISwims()
    {
        if (AITotal < playerTotal - BUFFER)
            AITotal += UnityEngine.Random.value * BONUS;
        else if (AITotal > playerTotal + BUFFER)
            AITotal += UnityEngine.Random.value * PENALTY;
        else
            AITotal += UnityEngine.Random.value * NORM;
    }

    private void PlayerSwims()
    {
        playerTotal += UnityEngine.Random.value * PLAYERSPEED;
    }

    private void PlayerLoses()
    {
        Debug.Log("Player Lost!");
        raceOver = true;
    }

    private void PlayerWins()
    {
        Debug.Log("Player Won!");
        raceOver = true;
    }

    private void RenderSwimmers()
    {
        Vector3 playerTarget = new Vector3((startPos + (endPos - startPos) * (playerTotal / WINCOND)), player.position.y, player.position.z);
        Vector3 enemyTarget = new Vector3((startPos + (endPos - startPos) * (AITotal / WINCOND)), enemy.position.y, enemy.position.z);

        player.position = Vector3.Lerp(player.position, playerTarget, .2f);
        enemy.position = Vector3.Lerp(enemy.position, enemyTarget, .2f);
    }
}