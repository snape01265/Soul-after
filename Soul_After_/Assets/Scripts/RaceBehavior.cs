using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class RaceBehavior : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public Transform startObj;
    public Transform endObj;
    public AudioSource countDownSFX;
    public AudioSource bgm;
    public BoolValue contestWon;

    private Animator playerAnim;
    private Animator enemyAnim;

    private readonly float PLAYERSPEED = 1.5f;
    private readonly float BUFFER = 5f;
    private readonly float BONUS = .3f;
    private readonly float NORM = .2f;
    private readonly float PENALTY = .1f;
    private readonly float WINCOND = 100f;

    private float startPos;
    private float endPos;
    private float playerTotal;
    private float AITotal;

    private bool raceStart;
    private bool raceOver;

    private SceneTransition sceneTransition;
    private Text timer;

    private void Awake()
    {
        contestWon.initialValue = false;
        raceStart = false;
        startPos = startObj.position.x;
        endPos = endObj.position.x;

        playerTotal = 0;
        AITotal = 0;
        //text's alpha value is set to 0 in the beginning.
        timer = GameObject.Find("Timer").GetComponent<Text>();
        Color timerColor = timer.color;
        timerColor.a = 0f;
        timer.color = timerColor;
        sceneTransition = GameObject.Find("Scene Transition").GetComponent<SceneTransition>();
        playerAnim = player.GetComponent<Animator>();
        enemyAnim = enemy.GetComponent<Animator>();
    }

    private void Update()
    {
        if (raceStart && !raceOver)
            if (Input.GetButtonDown("Horizontal"))
                PlayerSwims();
    }

    private void FixedUpdate()
    {
        if (raceStart && !raceOver)
        {
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
        if (AITotal < playerTotal - BUFFER || WINCOND - playerTotal < 10f)
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
        raceOver = true;
        DialogueLua.SetVariable("SwimGame.SwimGameWin", false);
        StartCoroutine(BackToScene());
    }

    private void PlayerWins()
    {
        raceOver = true;
        contestWon.initialValue = true;
        DialogueLua.SetVariable("SwimGame.SwimGameWin", true);
        StartCoroutine(BackToScene());
    }

    private void RenderSwimmers()
    {
        Vector3 playerTarget = new Vector3((startPos + (endPos - startPos) * (playerTotal / WINCOND)), player.position.y, player.position.z);
        Vector3 enemyTarget = new Vector3((startPos + (endPos - startPos) * (AITotal / WINCOND)), enemy.position.y, enemy.position.z);

        player.position = Vector3.Lerp(player.position, playerTarget, .05f);
        enemy.position = Vector3.Lerp(enemy.position, enemyTarget, .05f);

        if (!raceOver && Vector3.Distance(player.position, playerTarget) > .5f)
        {
            playerAnim.SetTrigger("Swim");
        }
    }

    private IEnumerator CountDown()
    {
        Color timerColor = timer.color;
        timerColor.a = 1f;
        timer.color = timerColor;
        countDownSFX.Play();
        int countDown = 3;
        while (countDown > 0)
        {
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
        enemyAnim.SetBool("Swim", true);
    }

    private IEnumerator BackToScene()
    {
        enemyAnim.SetBool("Swim", false);
        DialogueLua.SetVariable("SwimGame.SwimGameFin", true);
        yield return new WaitForSeconds(4f);
        sceneTransition.ChangeScene();
    }
}