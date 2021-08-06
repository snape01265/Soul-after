using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SajaPuzzleBehavior : MonoBehaviour
{
    public BoolList pressedStates;
    public VectorList birdPos;
    public BoolList boxOnBtn;
    public BoolValue puzzleFin;
    private readonly List<bool> FinishedState = new List<bool>(6) { true, true, true, true, true, true };
    private bool finished = false;
    private List<ButtonRenderer> btnRenders;
    private GameObject[] buttons;

    private void Awake()
    {
        if (!pressedStates.initialValue.SequenceEqual(pressedStates.defaultValue))
        {
            pressedStates.initialValue = new List<bool>(pressedStates.defaultValue);
        }

        if (!puzzleFin.initialValue)
        {
            GameObject.Find("그나마 정상인 놈").transform.Find("RPGTalk Area").GetComponent<RPGTalkArea>().enabled = false;
        }
        
        btnRenders = new List<ButtonRenderer>();
    }

    private void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("PressableObject");
        foreach (GameObject btn in buttons)
        {
            ButtonRenderer _btnRend = btn.GetComponent<ButtonRenderer>();
            if (_btnRend != null)
            {
                btnRenders.Add(_btnRend);
            }
        }
    }

    private void CheckFinished()
    {
        if (!finished && FinishedState.SequenceEqual(pressedStates.initialValue))
        {
            // finish event
            Debug.Log("Event Finished!");
            GameObject.Find("그나마 정상인 놈").transform.Find("RPGTalk Area").GetComponent<RPGTalkArea>().enabled = true;
            puzzleFin.initialValue = true;
            finished = true;
        } else
        {
            Debug.Log("Not finished");
        }
    }

    public void ResetPuzzle()
    {
        pressedStates.initialValue = new List<bool>(pressedStates.defaultValue);
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("MovableObject");
        
        for (int i = 0; i < 3; i++)
        {
            if (!birdPos.defaultPos[i].Equals(gameObjects[i].transform.position))
            {
                gameObjects[i].transform.position = birdPos.defaultPos[i];
            }
        }

        foreach (ButtonRenderer btnRend in btnRenders)
        {
            btnRend.Invoke("ButtonUp", 0f);
        }
    }

    public void AdvancePuzzle(int idx)
    {
        if (!finished)
        {
            //Debug.Log("Advance: idx = " + idx);

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
                    break;
            }

            CheckFinished();
        }
    }

    private void CrossFlip(int idx)
    {
        if (boxOnBtn.initialValue[idx] == false)
        {
            pressedStates.initialValue[idx] = false;
            btnRenders[idx].Invoke("ButtonUp", 0.1f);
        }
    }
}
