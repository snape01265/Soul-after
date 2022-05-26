using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class ChasemazePuzzleManager : MonoBehaviour
{
    public Player Player;
    public BoolList Progress;
    public Transform HubPos;
    public Transform StartPos;
    public SelectionDoorManager SelectionDoorManager;
    public Fadein Fadein;
    public PlayableDirector StartTimeline;
    public PlayableDirector EndTimeline;
    public CinemachineVirtualCamera ChaseCam;
    public MusicFade BGMToFade;
    public AudioSource ChaseBGM;
    [Header("Puzzle Settings")]
    public float FadeinDuration;
    public float CamshakeStrength = 1f;
    private List<Chasemaze_Hazard> hazard;
    private MusicFade chaseMusicFade;

    public void InitPuzzle()
    {
        if (ChaseBGM)
            chaseMusicFade = ChaseBGM.GetComponent<MusicFade>();
        hazard = new List<Chasemaze_Hazard>(GetComponentsInChildren<Chasemaze_Hazard>());
        hazard.Add(ChaseCam.GetComponent<Chasemaze_Hazard>());
        StartCoroutine(TeleToStartPos());
        Player.gameObject.GetComponent<PlayerHealth>().RestoreHealth();
    }

    public void FinPuzzle()
    {
        Progress.initialValue[1] = true;
        SelectionDoorManager.TrackProgress();
        StartCoroutine(TeletoHub());
    }

    public void StartPuzzle()
    {
        foreach (var haz in hazard)
        {
            haz.isMoving = true;
        }
        ChaseCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = CamshakeStrength;
    }

    public void EndPuzzle()
    {
        foreach (var haz in hazard)
        {
            haz.isMoving = false;
        }
        EndTimeline.Play();
    }

    IEnumerator TeleToStartPos()
    {
        Fadein.FadeInOutStatic(FadeinDuration);
        yield return new WaitForSeconds(FadeinDuration / 2);
        ChaseCam.enabled = true;
        Player.transform.position = StartPos.position;
        yield return new WaitForSeconds(FadeinDuration / 2);
        StartTimeline.Play();
    }

    IEnumerator TeletoHub()
    {
        Fadein.FadeInOutStatic(FadeinDuration);
        yield return new WaitForSeconds(FadeinDuration / 2);
        ChaseCam.enabled = false;
        Player.transform.position = HubPos.position;
        yield return null;
    }
}
