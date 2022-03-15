using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Playables;
using Cinemachine;

public class InvismazePuzzleManager : MonoBehaviour
{
    public GameObject Puzzle_1;
    public GameObject Puzzle_2;
    public GameObject Puzzle_3;
    public Player Player;
    public BoolList Progress;
    public Transform HubPos;
    public Transform StartPos;
    public SelectionDoorManager SelectionDoorManager;
    public Fadein Fadein;
    public PlayableDirector StartTimeline;
    public PlayableDirector EndTimeline;
    public Light2D Light;
    public CinemachineVirtualCamera InvisCam;
    [Header("Puzzle Settings")]
    public float BeforeFadeinDuration;
    public float FadeinDuration;
    public float LightsOutDuration;

    private Light2D playerLight;

    private void Start()
    {
        Player.transform.Find("Point Light 2D").gameObject.SetActive(true);
        playerLight = Player.transform.Find("Point Light 2D").GetComponent<Light2D>();
        playerLight.pointLightOuterRadius = 2;
        playerLight.intensity = 0;
    }

    public void InitPuzzle()
    {
        int mapNo = (int) Random.Range(0, 2);

        switch (mapNo)
        {
            case 0:
                Puzzle_1.SetActive(true);
                break;
            case 1:
                Puzzle_2.SetActive(true);
                break;
            case 2:
                Puzzle_3.SetActive(true);
                break;
        }

        Player.gameObject.GetComponent<PlayerHealth>().RestoreHealth();
        StartCoroutine(TeleToStartPos());
    }

    public void ReturnToStart()
    {
        StartCoroutine(ReturnToStartPos());
    }

    public void FinPuzzle()
    {
        Progress.initialValue[0] = true;
        SelectionDoorManager.TrackProgress();
        StartCoroutine(TeletoHub());
    }

    public void StartPuzzle()
    {
        StartCoroutine(LightsFade());
    }

    public void EndPuzzle()
    {
        EndTimeline.Play();
    }

    IEnumerator LightsFade()
    {
        Player.CancelControl();
        float time = 0;
        Light.intensity = 1;
        yield return new WaitForSeconds(BeforeFadeinDuration);
        while (Light.intensity >= .01f)
        {
            time += Time.deltaTime;
            Light.intensity = Mathf.Lerp(1, 0, time / LightsOutDuration);
            playerLight.intensity = Mathf.Lerp(0, 0.3f, time / LightsOutDuration);
            yield return new WaitForEndOfFrame();
        }
        Player.GiveBackControl();
        yield return new WaitForEndOfFrame();
    }

    IEnumerator TeleToStartPos()
    {
        Fadein.FadeInOutStatic(FadeinDuration);
        yield return new WaitForSeconds(FadeinDuration / 2);
        InvisCam.enabled = true;
        Player.transform.position = StartPos.position;
        yield return new WaitForSeconds(FadeinDuration / 2);
        StartTimeline.Play();
    }

    IEnumerator ReturnToStartPos()
    {
        Player.CancelControl();
        Fadein.FadeInOutStatic(FadeinDuration);
        yield return new WaitForSeconds(FadeinDuration / 2);
        Player.transform.position = StartPos.position;
        yield return new WaitForSeconds(FadeinDuration / 2);
        Player.GiveBackControl();
        yield return StartCoroutine(LightsFade());
    }

    IEnumerator TeletoHub()
    {
        Fadein.FadeInOutStatic(FadeinDuration);
        yield return new WaitForSeconds(FadeinDuration / 2);
        InvisCam.enabled = false;
        Player.transform.position = HubPos.position;
        yield return null;
    }
}
