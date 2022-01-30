using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxPuzzleManager : MonoBehaviour
{
    public KeyCode keyForMirror;
    public int turnCount;
    public int puzzleNum;

    private PushBox box;
    private GameObject player;
    private GameObject mainCamera;
    private bool isMirrorWorld = false;
    private bool isAvailable = true;
    private bool goalReached = false;
    private Vector3 startingBoxPos;
    private Vector3 startingPlayerPos;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        box = GameObject.FindGameObjectWithTag("PushBox").GetComponent<PushBox>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyForMirror) && isAvailable && !box.pushing)
        {
            if(!isMirrorWorld)
            {
                MirrorActivate(isMirrorWorld);
            }
            else if(isMirrorWorld)
            {
                MirrorActivate(isMirrorWorld);
            }
        }
        if (turnCount == 0)
        {
            switch (puzzleNum)
            {
                case 1:
                    StartCoroutine(Reset(12));
                    break;
                case 2:
                    StartCoroutine(Reset(9));
                    break;
                case 3:
                    StartCoroutine(Reset(2));
                    break;
            }
        }
        else if (turnCount >= 0 && goalReached)
        {
            switch (puzzleNum)
            {
                case 1:
                    nextPuzzle(puzzleNum);
                    break;
                case 2:
                    nextPuzzle(puzzleNum);
                    break;
            }
        }
    }
    public void MirrorActivate(bool mirrorWorld)
    {
        StartCoroutine(MirrorWorldTransition());
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        float playerZ = player.transform.position.z;
        float boxX = box.transform.position.x;
        float boxY = box.transform.position.y;
        float boxZ = box.transform.position.z;
        float camX = mainCamera.transform.position.x;
        float camY = mainCamera.transform.position.y;
        float camZ = mainCamera.transform.position.z;
        if (mirrorWorld == false)
        {
            player.transform.position = new Vector3(playerX + 22, playerY, playerZ);
            box.targetPos = new Vector3(boxX + 22, boxY, boxZ);
            box.transform.position = new Vector3(boxX + 22, boxY, boxZ);
            mainCamera.transform.position = new Vector3(camX + 22, camY, camZ);
            isMirrorWorld = true;
        }
        else
        {
            player.transform.position = new Vector3(playerX - 22, playerY, playerZ);
            box.targetPos = new Vector3(boxX - 22, boxY, boxZ);
            box.transform.position = new Vector3(boxX - 22, boxY, boxZ);
            mainCamera.transform.position = new Vector3(camX - 22, camY, camZ);
            isMirrorWorld = false;
        }
    }
    private void nextPuzzle(int puzzleNumber)
    {
        GameObject start = GameObject.Find("StartTile" + puzzleNum);
        float startX = start.transform.position.x;
        float startY = start.transform.position.y;
        float startZ = start.transform.position.z;

        player.transform.position = new Vector3(startX - 1, startY, startZ);
        box.targetPos = new Vector3(startX, startY, startZ);
        box.transform.position = new Vector3(startX, startY, startZ);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + 50, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }
    public void SetPosition()
    {
        box.targetPos = box.transform.position;
        box.LastLoc = box.transform.position;
        startingBoxPos = box.transform.position;
        startingPlayerPos = transform.Find("Player").position;
    }
    IEnumerator Reset(int turnLimit)
    {
        isAvailable = false;
        player.transform.position = new Vector3(startingPlayerPos.x, startingPlayerPos.y, startingPlayerPos.z);
        box.PushToDest(startingBoxPos);
        turnCount = turnLimit;
        //Play reset animation
        isAvailable = true;
        yield return null;
    }
    IEnumerator MirrorWorldTransition()
    {
        isAvailable = false;
        //Play some glowing animation
        isAvailable = true;
        yield return null;
    }
}
