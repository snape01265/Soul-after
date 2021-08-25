using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnHandler : MonoBehaviour
{
    private CameraShake shake;
    public bool FinishedTurn;
    public int level;
    public int attackType;
    public AudioSource dropSound;

    void Start()
    {
        FinishedTurn = false;
        dropSound = GameObject.Find("SFX/CrashSFX").GetComponent<AudioSource>();
        level = GameObject.FindGameObjectWithTag("GameController").GetComponent<TurnHandler>().phaseCount;
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
        GetComponent<Animator>().SetInteger("Level", level);
        GetComponent<Animator>().SetInteger("AtkType", attackType);
    }

    public void AtkDone()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<TurnHandler>().patternCount += 1;
        FinishedTurn = true;
    }
    public void NextPhase()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<TurnHandler>().PatternFinished();
    }
    public void CamShake()
    {
        shake.CamShake();
        if(dropSound != null)
        {
            dropSound.Play();
        }
    }
}
