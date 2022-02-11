using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxPuzzleManager_1 : MonoBehaviour
{
    public KeyCode keyForReset;
    public int turnCount;
    public int puzzleNum;
    public bool goalReached = false;
    public AudioSource ResetSFX;
    public Vector3 startingPlayerPos;
    [HideInInspector]
    public int goalCount;

    private readonly int[] goalCounts = new int[] {3, 3, 1};
    private List<Vector3> startingBoxPos = new List<Vector3>();
    private List<BoxPush_1> box = new List<BoxPush_1>();
    private GameObject player;
    private GameObject mainCamera;
    private bool isAvailable = true;
    private bool isPushing = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] pushboxes = GameObject.FindGameObjectsWithTag("PushBox");
        foreach(GameObject pushbox in pushboxes)
        {
            box.Add(pushbox.GetComponent<BoxPush_1>());
            startingBoxPos.Add(pushbox.transform.position);
        }
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        goalCount = goalCounts[0];
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

        if (Input.GetKeyDown(keyForReset) && isAvailable && !isPushing)
        {
            ResetSFX.Play();
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
            switch (puzzleNum)
            {
                case 1:
                    nextPuzzle("Start2");
                    goalCount = goalCounts[1];
                    goalReached = false;
                    break;
                case 2:
                    nextPuzzle("Start3");
                    goalCount = goalCounts[2];
                    goalReached = false;
                    break;
            }
        }
        else if (turnCount == 0)
        {
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

    private void nextPuzzle(string startNumber)
    {
        GameObject start = GameObject.Find(startNumber);
        float startX = start.transform.position.x;
        float startY = start.transform.position.y;
        float startZ = start.transform.position.z;

        player.transform.position = new Vector3(startX, startY, startZ);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + 50, mainCamera.transform.position.y, mainCamera.transform.position.z);
        puzzleNum += 1;
        SetPosition();
        turnCount = 0;
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
        player.transform.position = new Vector3(startingPlayerPos.x, startingPlayerPos.y, startingPlayerPos.z);
        for(int i = 0; i < box.Count; i++)
        {
            box[i].targetPos = box[i].transform.position;
            box[i].LastLoc = box[i].transform.position;
            box[i].PushToDest(startingBoxPos[i]);
        }

        turnCount = turnLimit;
        //Play reset animation
        isAvailable = true;
        yield return null;
    }
}
