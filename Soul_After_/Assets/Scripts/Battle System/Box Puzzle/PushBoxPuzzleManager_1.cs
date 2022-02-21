using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PushBoxPuzzleManager_1 : MonoBehaviour
{
    public KeyCode keyForReset;
    public FloatValue StageNo;
    [HideInInspector]
    public int turnCount;
    [HideInInspector]
    public int puzzleNum = 1;
    public bool goalReached = false;
    public AudioSource ResetSFX;
    public AudioSource OutofCountSFX;
    public AudioSource NextStageSFX;
    public PlayableDirector LastTimeline;
    [HideInInspector]
    public Vector3 startingPlayerPos;
    [HideInInspector]
    public int goalCount;
    [HideInInspector]
    public Fadein fade;
    public float fadeDuration;
    [HideInInspector]
    public bool isReset = false;
    [HideInInspector]
    public bool isPushing = false;

    private readonly int[] goalCounts = new int[] {3, 3, 1};
    private List<Vector3> startingBoxPos = new List<Vector3>();
    private List<BoxPush_1> box = new List<BoxPush_1>();
    private GameObject player;
    private GameObject mainCamera;
    private bool isAvailable = true;
    private bool isTranstioning = false;

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
                goalCount = goalCounts[1];
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

        if (Input.GetKeyDown(keyForReset) && isAvailable && !isPushing && !isReset)
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
        else if (turnCount >= 0 && goalReached && !isPushing)
        {
            goalReached = false;
            isTranstioning = true;
            switch (puzzleNum)
            {
                case 1:
                    nextPuzzle("Start2");
                    goalCount = goalCounts[1];
                    StageNo.initialValue = 1;
                    break;
                case 2:
                    nextPuzzle("Start3");
                    goalCount = goalCounts[2];
                    StageNo.initialValue = 2;
                    break;
                case 3:
                    fade.FadeInOutStatic(fadeDuration);
                    LastTimeline.Play();
                    break;
            }
        }
        else if (turnCount == 0 && !isReset && !isPushing && !isTranstioning)
        {
            if(OutofCountSFX)
                OutofCountSFX.Play();
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

    IEnumerator Reset(int turnLimit)
    {
        isAvailable = false;
        player.GetComponent<Player>().control = false;
        fade.FadeInOutStatic(fadeDuration);
        yield return new WaitForSeconds(0.5f);
        player.transform.position = new Vector3(startingPlayerPos.x, startingPlayerPos.y, startingPlayerPos.z);
        for (int i = 0; i < box.Count; i++)
        {
            //box[i].targetPos = box[i].transform.position;
            //box[i].LastLoc = box[i].transform.position;
            box[i].PushToDest(startingBoxPos[i]);
        }
        yield return new WaitForSeconds(fadeDuration - (fadeDuration / 2) + 1);
        player.GetComponent<Player>().control = true;
        isAvailable = true;
        turnCount = turnLimit;
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
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + (30 * CamMulti), mainCamera.transform.position.z);

        for (int i = 0; i < box.Count; i++)
        {
            //box[i].targetPos = box[i].transform.position;
            //box[i].LastLoc = box[i].transform.position;
            box[i].PushToDest(startingBoxPos[i]);
        }
        yield return new WaitForSeconds(fadeDuration - (fadeDuration / 2) + 1);
        player.GetComponent<Player>().control = true;
        isAvailable = true;
        turnCount = turnLimit;
        SetPosition();
        isReset = false;
        isTranstioning = false;
        yield return null;
    }
}
