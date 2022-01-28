using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadein : MonoBehaviour
{
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;

    private Transform player;
    private Vector3 pos = Vector3.zero;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    public void FadeInOut(float Time)
    {
        StartCoroutine(FIO(Time));
    }

    public void SetPosX(float XPos)
    {
        pos = new Vector3(XPos, pos.y, pos.z);
    }

    public void SetPosY(float YPos)
    {
        pos = new Vector3(pos.x, YPos, pos.z);
    }

    private IEnumerator FIO(float time)
    {
        if (fadeOutPanel != null)
        {
            GameObject panel = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1);
            yield return new WaitForSeconds(time);
        }
        player.position = pos;
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1);
        }
    }
}