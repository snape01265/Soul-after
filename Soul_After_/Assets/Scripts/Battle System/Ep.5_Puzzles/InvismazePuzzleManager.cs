using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

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
    public Light2D Light;
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
        TeletoStart();
    }

    public void TeletoStart()
    {
        StartCoroutine(TeletoStartPos());
    }

    public void FinPuzzle()
    {
        Progress.initialValue[0] = true;
        SelectionDoorManager.TrackProgress();
        StartCoroutine(TeletoHub());
    }

    private void LightsOut()
    {
        StartCoroutine(LightsFade());
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
        yield return null;
    }

    IEnumerator TeletoStartPos()
    {
        Fadein.FadeInOutStatic(FadeinDuration);
        yield return new WaitForSeconds(FadeinDuration / 2);
        Player.transform.position = StartPos.position;
        yield return null;
        yield return new WaitForSeconds(FadeinDuration / 2);
        LightsOut();
    }

    IEnumerator TeletoHub()
    {
        Fadein.FadeInOutStatic(FadeinDuration);
        yield return new WaitForSeconds(FadeinDuration / 2);
        Player.transform.position = HubPos.position;
        yield return null;
    }
}
