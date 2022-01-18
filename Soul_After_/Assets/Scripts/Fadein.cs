using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadein : MonoBehaviour
{
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;

    private void Awake()
    {
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

    private IEnumerator FIO(float time)
    {
        if (fadeOutPanel != null)
        {
            GameObject panel = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1);
            yield return new WaitForSeconds(time);
        }
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1);
        }
    }
}
