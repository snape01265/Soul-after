using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PushBoxPuzzleManager : MonoBehaviour
{
    public KeyCode keyForMirror;
    public KeyCode keyForReset;
    [HideInInspector]
    public int TurnCount
    {
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
    public FloatValue puzzleSave;
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
    public PlayableDirector LastTimeline;
    public AudioSource resetSFX;
    public AudioSource mirrorSFX;
    public AudioSource clearSFX;
    public float fadeDuration;
    public Text CounterTxt;
    [HideInInspector]
    public bool isReset = false;

    private PushBox box;
    private GameObject player;
    private GameObject mainCamera;
    private bool isMirrorWorld = false;
    private bool isAvailable = true;
    private bool timelinePlaying = false;

    private void Start()
    {
        fade = GameObject.Find("Fadein").GetComponent<Fadein>();
        player = GameObject.FindGameObjectWithTag("Player");
        box = GameObject.FindGameObjectWithTag("PushBox").GetComponent<PushBox>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        puzzleNum = (int) puzzleSave.initialValue;
        clearSFX.Play();
        switch (puzzleNum)
        {
            case 0:
                StartCoroutine(nextPuzzle("Start1", 13));
                break;
            case 1:
                StartCoroutine(nextPuzzle("Start2", 8));
                break;
            case 2:
                StartCoroutine(nextPuzzle("Start3", 3));
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyForMirror) && isAvailable && !box.pushing && !timelinePlaying)
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
        else if (Input.GetKeyDown(keyForReset) && isAvailable && !box.pushing && !isReset)
        {
            resetSFX.Play();
            isReset = true;
            switch (puzzleNum)
            {
                case 0:
                    StartCoroutine(Reset(13));
                    break;
                case 1:
                    StartCoroutine(Reset(8));
                    break;
                case 2:
                    StartCoroutine(Reset(3));
                    break;
            }
        }
        else if (TurnCount >= 0 && goalReached && !box.pushing)
        {
            clearSFX.Play();
            isReset = true;
            switch (puzzleNum)
            {
                case 0:
                    StartCoroutine(nextPuzzle("Start2", 8));
                    goalReached = false;
                    puzzleNum += 1;
                    puzzleSave.initialValue = puzzleNum;
                    break;
                case 1:
                    StartCoroutine(nextPuzzle("Start3", 3));
                    goalReached = false;
                    puzzleNum += 1;
                    puzzleSave.initialValue = puzzleNum;
                    break;
                case 2:
                    fade.FadeInOutStatic(fadeDuration);
                    LastTimeline.Play();
                    goalReached = false;
                    break;
            }
        }
        else if (TurnCount == 0 && !isReset && !box.pushing && !goalReached)
        {
            resetSFX.Play();
            isReset = true;
            switch (puzzleNum)
            {
                case 0:
                    StartCoroutine(Reset(13));
                    break;
                case 1:
                    StartCoroutine(Reset(8));
                    break;
                case 2:
                    StartCoroutine(Reset(3));
                    break;
            }
        }
    }
    public void MirrorActivate(bool mirrorWorld)
    {
        StartCoroutine(MirrorWorldTransition(mirrorWorld));
    }

    IEnumerator nextPuzzle(string startNumber, int turnLimit)
    {
        GameObject start = GameObject.Find(startNumber);
        float startX = start.transform.position.x;
        float startY = start.transform.position.y;
        float startZ = start.transform.position.z;

        isAvailable = false;
        player.GetComponent<Player>().control = false;
        fade.FadeInOutStatic(fadeDuration);
        yield return new WaitForSeconds(0.5f);
        player.transform.position = new Vector3(startX - 1, startY, startZ);
        box.targetPos = new Vector3(startX, startY, startZ);
        box.transform.position = new Vector3(startX, startY, startZ);
        switch (puzzleNum)
        {
            case 0:
                mainCamera.transform.position = new Vector3(3f, -0.5f, -10f);
                break;
            case 1:
                mainCamera.transform.position = new Vector3(53f, -0.5f, -10f);
                break;
            case 2:
                mainCamera.transform.position = new Vector3(103f, -0.5f, -10f);
                break;
        }
        SetPosition();
        yield return new WaitForSeconds(fadeDuration - 0.5f);
        if(isMirrorWorld)
        {
            isMirrorWorld = false;
        }
        isReset = false;
        isAvailable = true;
        player.GetComponent<Player>().control = true;
        TurnCount = turnLimit;
        yield return null;
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
        box.targetPos = startingBoxPos;
        box.transform.position = startingBoxPos;
        if (isMirrorWorld)
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x - 22, mainCamera.transform.position.y, mainCamera.transform.position.z);
            isMirrorWorld = false;
        }
        yield return new WaitForSeconds(fadeDuration - 0.5f);
        player.GetComponent<Player>().control = true;
        isAvailable = true;
        TurnCount = turnLimit;
        isReset = false;
        yield return null;
    }
    IEnumerator MirrorWorldTransition(bool mirrorWorld)
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, player.transform.position);
        RaycastHit2D boxHit = Physics2D.Raycast(player.transform.position, player.transform.position);
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
            if (hit.collider != null && hit.transform.gameObject.GetComponent<PortalActive>())
            {
                player.transform.position = new Vector3(hit.transform.gameObject.transform.position.x + 23, hit.transform.gameObject.transform.position.y, playerZ);
            }
            isMirrorWorld = true;
        }
        else
        {
            player.transform.position = new Vector3(playerX - 22, playerY, playerZ);
            box.targetPos = new Vector3(boxX - 22, boxY, boxZ);
            box.transform.position = new Vector3(boxX - 22, boxY, boxZ);
            mainCamera.transform.position = new Vector3(camX - 22, camY, camZ);
            if (hit.collider != null && hit.transform.gameObject.GetComponent<PortalActive>())
            {
                player.transform.position = new Vector3(hit.transform.gameObject.transform.position.x - 21, hit.transform.gameObject.transform.position.y, playerZ);
            }
            isMirrorWorld = false;
        }
        yield return new WaitForSeconds(fadeDuration - 0.5f);
        player.GetComponent<Player>().control = true;
        isAvailable = true;
        yield return null;
    }

    public void SetCounter(int count)
    {
        CounterTxt.text = "���� �� : " + count.ToString();
    }

    public void SetTimelineBoolValue(bool playing)
    {
        timelinePlaying = playing;
    }
}
