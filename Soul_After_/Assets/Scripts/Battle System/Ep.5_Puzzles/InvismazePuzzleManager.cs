using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InvismazePuzzleManager : MonoBehaviour
{
    public Player Player;
    public Transform StartPos;
    public Fadein Fadein;
    public float FadeinDuration;
    public float LightsOutDuration;
    public Light2D Light;

    private void Start()
    {
        Player.transform.position = StartPos.position;

        int mapNo = (int) Random.Range(0, 2);

        switch (mapNo)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TeletoStart(FadeinDuration));
        }
    }

    public void LightsOut()
    {
        StartCoroutine(LightsOut(LightsOutDuration));
    }

    IEnumerator LightsOut(float duration)
    {
        while (Light.intensity >= .01f)
        {
            Light.intensity = Mathf.Lerp(Light.intensity, 0, duration);
        }
        yield return null;
    }

    IEnumerator TeletoStart(float duration)
    {
        Fadein.FadeInOutStatic(duration);
        yield return new WaitForSeconds(duration/2);
        Player.transform.position = StartPos.position;
        yield return null;
    }
}
