using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceBehavior : MonoBehaviour
{
    private readonly float PLAYERSPEED = 1.4f;
    private readonly float BUFFER = 5f;
    private readonly float BONUS = .12f;
    private readonly float NORM = .08f;
    private readonly float PENALTY = .06f;
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

    private SceneTransition sceneTransition;
    private Text timer;
    //Audio Source
    public AudioSource countDownSFX;
    public AudioSource bgm;

    private void Awake()
    {
        raceStart = false;
        startPos = startObj.position.x;
        endPos = endObj.position.x;

        playerTotal = 0;
        AITotal = 0;
        //text's alpha value is set to 0 in the beginning.
        timer = GameObject.Find("Timer").GetComponent<Text>();
        var timerColor = timer.color;
        timerColor.a = 0f;
        timer.color = timerColor;
        sceneTransition = GameObject.Find("Scene Transition").GetComponent<SceneTransition>();
    }

    private void Update()
    {
        if (raceStart && !raceOver)
        {
            if (Input.GetButtonDown("Horizontal"))
                PlayerSwims();
            AISwims();
            RenderSwimmers();
            CheckRaceFinish();
        }
    }

    public void StartRace()
    {
        StartCoroutine(CountDown());
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
        StartCoroutine(BackToScene());
    }

    private void PlayerWins()
    {
        Debug.Log("Player Won!");
        raceOver = true;
        StartCoroutine(BackToScene());
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
        countDownSFX.Play();
        int countDown = 3;
        while (countDown > 0)
        {
            timer = GameObject.Find("Timer").GetComponent<Text>();
            var timerColor = timer.color;
            timerColor.a = 1f;
            timer.color = timerColor;
            timer.text = countDown.ToString();
            countDown -= 1;
            yield return new WaitForSeconds(1.3f);
        }
        timer.text = "GO!";
        yield return new WaitForSeconds(1.5f);
        timer.text = "";
        //After "Go!" is shown on the screen, the text should disappear and the bgm should play. 
        yield return raceStart = true;
        bgm.Play();
    }

    private IEnumerator BackToScene()
    {
        yield return new WaitForSeconds(4f);
        sceneTransition.ChangeScene();
    }
}