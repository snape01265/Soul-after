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
    public Fadein Fadein;
    public float BeforeFadeinDuration;
    public float FadeinDuration;
    public float LightsOutDuration;
    public Light2D Light;

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

        TeletoStart();
    }

    public void TeletoStart()
    {
        StartCoroutine(TeletoStartPos());
    }

    public void FinPuzzle()
    {
        Progress.initialValue[1] = true;
        StartCoroutine(TeletoHub());
    }

    private void LightsOut()
    {
        StartCoroutine(LightsFade());
    }

    IEnumerator LightsFade()
    {
        Light.intensity = 1;
        yield return new WaitForSeconds(BeforeFadeinDuration);
        while (Light.intensity >= .01f)
        {
            Light.intensity = Mathf.Lerp(Light.intensity, 0, LightsOutDuration);
        }
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
