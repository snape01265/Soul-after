using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceBehavior : MonoBehaviour
{
    private readonly float PLAYERSPEED = 2f;
    private readonly float BUFFER = 5f;
    private readonly float BONUS = .1f;
    private readonly float NORM = .08f;
    private readonly float PENALTY = .05f;
    private readonly float WINCOND = 100f;

    public Transform player;
    public Transform enemy;
    public Transform startObj;
    public Transform endObj;

    private float startPos;
    private float endPos;
    private float playerTotal;
    private float AITotal;

    private bool raceStart;
    private bool raceOver;

    private Text timer;

    private void Awake()
    {
        raceStart = false;
        startPos = startObj.position.x;
        endPos = endObj.position.x;

        playerTotal = 0;
        AITotal = 0;

        timer = GameObject.Find("Timer").GetComponent<Text>();
    }

    private void Update()
    {
        if (raceStart && !raceOver)
        {
            if (Input.GetButtonDown("Jump"))
                PlayerSwims();
            AISwims();
            RenderSwimmers();
            CheckRaceFinish();
        }
    }

    public void StartRace()
    {
        StartCoroutine("CountDown");
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

        player.position = Vector3.Lerp(player.position, playerTarget, .05f);
        enemy.position = Vector3.Lerp(enemy.position, enemyTarget, .05f);
    }

    private IEnumerator CountDown()
    {
        int countDown = 5;
        while (countDown > 0)
        {
            timer.text = countDown.ToString();
            countDown -= 1;
            yield return new WaitForSeconds(1);
        }
        timer.text = "GO!";
        yield return raceStart = true;
    }
}