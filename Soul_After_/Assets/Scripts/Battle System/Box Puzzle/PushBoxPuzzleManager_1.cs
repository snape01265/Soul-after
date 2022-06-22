using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using System;

public class PushBoxPuzzleManager_1 : MonoBehaviour
{
    public KeyCode keyForReset;
    public FloatValue StageNo;
    [HideInInspector]
    public int TurnCount {
        get
        {
            return turnCount;
        }
        set
        {
            turnCount = value;
            SetCounter(value);
        }
    }
    private int turnCount = 1;
    [NonSerialized]
    public int puzzleNum = 1;
    public bool goalReached = false;
    public AudioSource ResetSFX;
    public AudioSource OutofCountSFX;
    public AudioSource NextStageSFX;
    public PlayableDirector LastTimeline;
    public Text CounterTxt;
    [NonSerialized]
    public Vector3 startingPlayerPos;
    [NonSerialized]
    public int goalCount;
    [NonSerialized]
    public Fadein fade;
    public float fadeDuration;
    [NonSerialized]
    public bool isAvailable = true;
    [NonSerialized]
    public bool isPushing = false;
    [NonSerialized]
    public bool isReset = false;

    private readonly int[] goalCounts = new int[] {3, 3, 1};
    private List<Vector3> startingBoxPos = new List<Vector3>();
    private List<BoxPush_1> box = new List<BoxPush_1>();
    private GameObject player;
    private GameObject mainCamera;
    private bool isTranstioning = false;
    private bool puzzleFinished = false;

    private void Start()
    {
        fade = GameObject.Find("Fadein").GetComponent<Fadein>();
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] pushboxes = GameObject.FindGameObjectsWithTag("PushBox");
        foreach(GameObject pushbox in pushboxes)
        {
            box.Add(pushbox.GetComponent<BoxPush_1>());
            startingBoxPos.Add(pushbox.transform.position);
        }
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        puzzleNum = (int) StageNo.initialValue;
        goalCount = goalCounts[puzzleNum];
        switch (puzzleNum)
        {
            case 0:
                nextPuzzle("Start1", 1);
                goalCount = goalCounts[0];
                break;
            case 1:
                nextPuzzle("Start2", 2);
                goalCount = goalCounts[1];
                break;
            case 2:
                nextPuzzle("Start3", 3);
                goalCount = goalCounts[2];
                break;
        }
        SetPosition();
    }

    private void Update()
    {
        if (puzzleFinished)
        {
            return;
        }

        foreach(BoxPush_1 pb in box)
        {
            if (pb.pushing)
            {
                isPushing = true;
                break;
            }
            isPushing = false;
        }

        if (goalCount <= 0)
            goalReached = true;

        if (isPushing || isTranstioning)
        {
            return;
        }

        if (Input.GetKeyDown(keyForReset) && isAvailable && !isReset)
        {
            ResetSFX.Play();
            isReset = true;
            switch (puzzleNum)
            {
                case 1:
                    StartCoroutine(Reset(18));
                    break;
                case 2:
                    StartCoroutine(Reset(27));
                    break;
                case 3:
                    StartCoroutine(Reset(2));
                    break;
            }
        }
        else if (TurnCount >= 0 && goalReached)
        {
            goalReached = false;
            isTranstioning = true;
            switch (puzzleNum)
            {
                case 1:
                    nextPuzzle("Start2");
                    goalCount = goalCounts[1];
                    StageNo.initialValue = 1;
                    return;
                case 2:
                    nextPuzzle("Start3");
                    goalCount = goalCounts[2];
                    StageNo.initialValue = 2;
                    return;
                case 3:
                    puzzleFinished = true;
                    fade.FadeInOutStatic(fadeDuration);
                    LastTimeline.Play();
                    return;
            }
        }
        else if (TurnCount == 0 && !isReset)
        {
            if(OutofCountSFX)
                OutofCountSFX.Play();
            isReset = true;
            switch (puzzleNum)
            {
                case 1:
                    StartCoroutine(Reset(18));
                    return;
                case 2:
                    StartCoroutine(Reset(27));
                    return;
                case 3:
                    StartCoroutine(Reset(2));
                    return;
            }
        }
    }

    private void nextPuzzle(string startNumber, int PuzzleNumber = -1)
    {
        GameObject start = GameObject.Find(startNumber);
        float startX = start.transform.position.x;
        float startY = start.transform.position.y;
        float startZ = start.transform.position.z;
        if (PuzzleNumber == -1)
            puzzleNum += 1;
        else
            puzzleNum = PuzzleNumber;

        switch (puzzleNum)
        {
            case 1:
                StartCoroutine(NextLevel(18, startX, startY, startZ, 0));
                break;
            case 2:
                StartCoroutine(NextLevel(27, startX, startY, startZ, 1));
                break;
            case 3:
                StartCoroutine(NextLevel(2, startX, startY, startZ, 2));
                break;
        }
    }

    public void SetPosition()
    {
        foreach(BoxPush_1 pb in box)
        {
            pb.targetPos = pb.transform.position;
            pb.LastLoc = pb.transform.position;
        }
        startingPlayerPos = player.transform.position;
    }

    public void SetCounter(int count)
    {
        CounterTxt.text = "≥≤¿∫ ≈œ : " + count.ToString();
    }

    IEnumerator Reset(int turnLimit)
    {
        isAvailable = false;
        player.GetComponent<Player>().control = false;
        fade.FadeInOutStatic(fadeDuration);
        yield return new WaitForSeconds(0.5f);
        player.transform.position = new Vector3(startingPlayerPos.x, startingPlayerPos.y, startingPlayerPos.z);
        for (int i = 0; i < box.Count; i++)
        {
            box[i].PushToDest(startingBoxPos[i], false);
        }
        yield return new WaitForSeconds(fadeDuration - (fadeDuration / 2) + 1);
        player.GetComponent<Player>().control = true;
        isAvailable = true;
        TurnCount = turnLimit;
        isReset = false;
        yield return null;
    }

    IEnumerator NextLevel(int turnLimit, float startX, float startY, float startZ, int CamMulti)
    {
        if (NextStageSFX != null)
            NextStageSFX.Play();
        isAvailable = false;
        player.GetComponent<Player>().control = false;
        fade.FadeInOutStatic(fadeDuration);
        yield return new WaitForSeconds(0.5f);
        player.transform.position = new Vector3(startX, startY, startZ);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, 1 + (30 * CamMulti), mainCamera.transform.position.z);
        for (int i = 0; i < box.Count; i++)
        {
            box[i].PushToDest(startingBoxPos[i], false);
        }
        yield return new WaitForSeconds(fadeDuration - (fadeDuration / 2) + 1);
        player.GetComponent<Player>().control = true;
        isAvailable = true;
        TurnCount = turnLimit;
        SetPosition();
        isReset = false;
        isTranstioning = false;
        yield return null;
    }
}
