using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasemazePuzzleManager : MonoBehaviour
{
    public Chasemaze_Hazard Hazard;
    public Player Player;
    public BoolList Progress;
    public Transform HubPos;
    public Transform StartPos;
    public Fadein Fadein;
    [Header("Puzzle Settings")]
    public float ChaseStartTime;
    public float FadeinDuration;

    public void InitPuzzle()
    {
        StartCoroutine(StartChase());
        Player.gameObject.GetComponent<PlayerHealth>().RestoreHealth();
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
