using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class SajaPuzzleBehavior : MonoBehaviour
{
    [HideInInspector]
    public List<bool> pressedStates;
    [HideInInspector]
    public List<bool> boxOnBtn;

    public BoolValue puzzleFin;

    private readonly Vector3 REGBIRDPOS = new Vector3(-5, 3, 0);
    private readonly Vector3 DUMBIRDPOS = new Vector3(0, 3, 0);
    private readonly Vector3 FORGETBIRDPOS = new Vector3(5, 3, 0);
    private readonly List<bool> FINISHEDSTATE = new List<bool>(6) { true, true, true, true, true, true };

    private bool finished = false;
    private List<ButtonRenderer> btnRenders;
    private GameObject[] buttons;
    public AudioSource _audio;


    private void Awake()
    {
        pressedStates = new List<bool>(6) { false, false, false, false, false, false };
        boxOnBtn = new List<bool>(6) { false, false, false, false, false, false };
        /*
        if (!puzzleFin.initialValue)
        {
            //need to switch to dialogue system variable. 
            GameObject.Find("그나마 정상인 놈").transform.Find("RPGTalk Area").GetComponent<RPGTalkArea>().enabled = true;
        } else GameObject.Find("Road Block").SetActive(true);
        */
        btnRenders = new List<ButtonRenderer>();
    }

    private void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("PressableObject");
        btnRenders = new List<ButtonRenderer>(6) { null, null, null, null, null, null };
        foreach (GameObject btn in buttons)
        {
            ButtonRenderer _btnRend = btn.GetComponent<ButtonRenderer>();
            if (_btnRend != null)
            {
                char a = btn.name[btn.name.Length - 1];
                int idx = int.Parse(a.ToString()) - 1;
                btnRenders[idx] = _btnRend;
            }
        }
    }

    private void CheckFinished()
    {
        if (!finished && FINISHEDSTATE.SequenceEqual(pressedStates))
        {
            // finish event
            Debug.Log("Event Finished!");
            //GameObject.Find("그나마 정상인 놈").transform.Find("RPGTalk Area").gameObject.SetActive(false);
            //GameObject.Find("그나마 정상인 놈").transform.Find("RPGTalk Area (Suit On)").gameObject.SetActive(true);
            //puzzleFin.initialValue = true;
            DialogueLua.SetVariable("PuzzleFinished_Saja", true);
            DialogueLua.SetVariable("TimelineToPlay", 3);
            finished = true;
            _audio.Play();
        } else
        {
            Debug.Log("Not finished");
        }
    }

    public void ResetPuzzle()
    {
        pressedStates = new List<bool>(6) { false, false, false, false, false, false };
        boxOnBtn = new List<bool>(6) { false, false, false, false, false, false };
        finished = false;

        GameObject.Find("등록시키고 싶은 새").transform.position = REGBIRDPOS;
        GameObject.Find("멀뚱 멀뚱한 새").transform.position = DUMBIRDPOS;
        GameObject.Find("항상 까먹는 새").transform.position = FORGETBIRDPOS;

        foreach (ButtonRenderer btnRend in btnRenders)
        {
            btnRend.ButtonUp();
        }
    }

    public void AdvancePuzzle(int idx)
    {
        if (!finished)
        {
            pressedStates[idx] = true;
            Debug.Log(idx);
            switch (idx)
            {
                case 0:
                    {                     
                        CrossFlip(1);
                        CrossFlip(3);
                        break;
                    }
                case 1:
                    {
                        CrossFlip(0);
                        CrossFlip(2);
                        CrossFlip(4);
                        break;
                    }
                case 2:
                    {
                        CrossFlip(1);
                        CrossFlip(5);
                        break;
                    }
                case 3:
                    {
                        CrossFlip(0);
                        CrossFlip(4);
                        break;
                    }
                case 4:
                    {
                        CrossFlip(1);
                        CrossFlip(3);
                        CrossFlip(5);
                        break;
                    }
                case 5:
                    {
                        CrossFlip(2);
                        CrossFlip(4);
                        break;
                    }
                default:
                    {
                        Debug.LogWarning("Some problem at SajaPuzleBehavior.cs!");
                        break;
                    }
            }

            CheckFinished();
        }
    }

    private void CrossFlip(int idx)
    {
        if (boxOnBtn[idx] == false)
        {
            pressedStates[idx] = false;
            btnRenders[idx].ButtonUp();
        }
    }

    public void TogglePressedState(int idx)
    {
        pressedStates[idx] = !pressedStates[idx];
    }

    public void ToggleBoxOnBtn(int idx)
    {
        boxOnBtn[idx] = !boxOnBtn[idx];
    }
}
