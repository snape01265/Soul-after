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
    public AudioSource _audio;

    private void Awake()
    {
        boxOnBtn.initialValue = new List<bool>(6) { false, false, false, false, false, false };
        if (!pressedStates.initialValue.SequenceEqual(pressedStates.defaultValue))
        {
            pressedStates.initialValue = new List<bool>(pressedStates.defaultValue);
        }

        if (!puzzleFin.initialValue)
        {
            GameObject.Find("�׳��� ������ ��").transform.Find("RPGTalk Area").GetComponent<RPGTalkArea>().enabled = true;
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
                GameObject.Find("Road Block").SetActive(true);
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
            GameObject.Find("�׳��� ������ ��").transform.Find("RPGTalk Area").gameObject.SetActive(false);
            GameObject.Find("�׳��� ������ ��").transform.Find("RPGTalk Area (Suit On)").gameObject.SetActive(true);
            puzzleFin.initialValue = true;
            finished = true;
            _audio.Play();
        } else
        {
            Debug.Log("Not finished");
        }
    }

    public void ResetPuzzle()
    {
        finished = false;
        pressedStates.initialValue = new List<bool>(pressedStates.defaultValue);

        GameObject.Find("��Ͻ�Ű�� ���� ��").transform.position = birdPos.defaultPos[0];
        GameObject.Find("�ֶ� �ֶ��� ��").transform.position = birdPos.defaultPos[1];
        GameObject.Find("�׻� ��Դ� ��").transform.position = birdPos.defaultPos[2];


        foreach (ButtonRenderer btnRend in btnRenders)
        {
            btnRend.ButtonUp();
        }
    }

    public void AdvancePuzzle(int idx)
    {
        if (!finished)
        {
            pressedStates.initialValue[idx] = true;

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
        if (boxOnBtn.initialValue[idx] == false)
        {
            pressedStates.initialValue[idx] = false;
            btnRenders[idx].ButtonUp();
        }
    }
}
