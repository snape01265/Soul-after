using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasemazePuzzleManager : MonoBehaviour
{
    public Player Player;
    public Chasemaze_Hazard Hazard;
    public BoolList Progress;
    public Transform HubPos;
    public Transform StartPos;
    public Fadein Fadein;
    public float ChaseStartTime;
    public float FadeinDuration;

    public void InitPuzzle()
    {
        StartCoroutine(StartChase());
    }

    public void FinPuzzle()
    {
        Hazard.isMoving = false;
        Progress.initialValue[1] = true;
        StartCoroutine(TeletoHub());
    }

    IEnumerator StartChase()
    {
        Fadein.FadeInOutStatic(FadeinDuration);
        yield return new WaitForSeconds(FadeinDuration / 2);
        Player.transform.position = StartPos.position;
        yield return null;
        yield return new WaitForSeconds(FadeinDuration / 2);
        yield return new WaitForSeconds(ChaseStartTime);
        Hazard.isMoving = true;
    }

    IEnumerator TeletoHub()
    {
        Fadein.FadeInOutStatic(FadeinDuration);
        yield return new WaitForSeconds(FadeinDuration / 2);
        Player.transform.position = HubPos.position;
        yield return null;
    }
}
