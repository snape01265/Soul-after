using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxPuzzleManager : MonoBehaviour
{
    public KeyCode keyForMirror;
    public KeyCode keyForReset;
    [HideInInspector]
    public int turnCount;
    [HideInInspector]
    public int puzzleNum;
    [HideInInspector]
    public bool goalReached = false;
    [HideInInspector]
    public Vector3 startingBoxPos;
    [HideInInspector]
    public Vector3 startingPlayerPos;
    [HideInInspector]
    public Fadein fade;
    public AudioSource resetSFX;
    public AudioSource mirrorSFX;
    public int fadeDuration;

    private PushBox box;
    private GameObject player;
    private GameObject mainCamera;
    private bool isMirrorWorld = false;
    private bool isAvailable = true;

    private void Start()
    {
        fade = GameObject.Find("Fadein").GetComponent<Fadein>();
        player = GameObject.FindGameObjectWithTag("Player");
        box = GameObject.FindGameObjectWithTag("PushBox").GetComponent<PushBox>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        SetPosition();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyForMirror) && isAvailable && !box.pushing)
        {
            mirrorSFX.Play();
            if (!isMirrorWorld)
            {
                MirrorActivate(isMirrorWorld);
            }
            else if(isMirrorWorld)
            {
                MirrorActivate(isMirrorWorld);
            }
        }
        else if (Input.GetKeyDown(keyForReset) && isAvailable && !box.pushing)
        {
            resetSFX.Play();
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
        else if (turnCount == 0)
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

        else if (turnCount >= 0 && goalReached && !box.pushing)
        {
            switch (puzzleNum)
            {
                case 1:
                    nextPuzzle("Start2");
                    goalReached = false;
                    break;
                case 2:
                    nextPuzzle("Start3");
                    goalReached = false;
                    break;
            }
        }
    }
    public void MirrorActivate(bool mirrorWorld)
    {
        StartCoroutine(MirrorWorldTransition(mirrorWorld));
    }

    private void nextPuzzle(string startNumber)
    {
        GameObject start = GameObject.Find(startNumber);
        float startX = start.transform.position.x;
        float startY = start.transform.position.y;
        float startZ = start.transform.position.z;

        player.transform.position = new Vector3(startX - 1, startY, startZ);
        box.targetPos = new Vector3(startX, startY, startZ);
        box.transform.position = new Vector3(startX, startY, startZ);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + 50, mainCamera.transform.position.y, mainCamera.transform.position.z);
        puzzleNum += 1;
        SetPosition();
        turnCount = 0;
    }
    public void SetPosition()
    {
        box.targetPos = box.transform.position;
        box.LastLoc = box.transform.position;
        startingBoxPos = box.transform.position;
        startingPlayerPos = player.transform.position;
    }
    IEnumerator Reset(int turnLimit)
    {
        isAvailable = false;
        player.GetComponent<Player>().control = false;
        fade.FadeInOutStatic(fadeDuration);
        yield return new WaitForSeconds(0.5f);
        player.transform.position = new Vector3(startingPlayerPos.x, startingPlayerPos.y, startingPlayerPos.z);
        box.PushToDest(startingBoxPos);
        if(isMirrorWorld)
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x - 22, mainCamera.transform.position.y, mainCamera.transform.position.z);
            isMirrorWorld = false;
        }
        yield return new WaitForSeconds(fadeDuration - fadeDuration/2);
        player.GetComponent<Player>().control = true;
        isAvailable = true;
        turnCount = turnLimit;
        yield return null;
    }
    IEnumerator MirrorWorldTransition(bool mirrorWorld)
    {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        float playerZ = player.transform.position.z;
        float boxX = box.transform.position.x;
        float boxY = box.transform.position.y;
        float boxZ = box.transform.position.z;
        float camX = mainCamera.transform.position.x;
        float camY = mainCamera.transform.position.y;
        float camZ = mainCamera.transform.position.z;
        isAvailable = false;
        player.GetComponent<Player>().control = false;
        fade.FadeInOutStatic(fadeDuration);
        yield return new WaitForSeconds(0.5f);
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
        yield return new WaitForSeconds(fadeDuration - fadeDuration/2);
        player.GetComponent<Player>().control = true;
        isAvailable = true;
        yield return null;
    }
}
