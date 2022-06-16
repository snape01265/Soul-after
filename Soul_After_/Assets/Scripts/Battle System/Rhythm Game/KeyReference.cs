using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyReference : MonoBehaviour
{
    private GameManager gameManager;
    private bool keyShow = false;
    void Start()
    {
        gameManager = GameObject.Find("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(gameManager.isPlaying && !keyShow)
        {
            ShowKeys();
        }
    }
    public void ShowKeys()
    {
        IEnumerator KeyFade()
        {
            StartCoroutine(FadeInOut(false));
            yield return new WaitForSeconds(3.5f);
            StartCoroutine(FadeInOut(true));
        }
        StartCoroutine(KeyFade());
        keyShow = true;
    }
    IEnumerator FadeInOut(bool fadeaway)
    {
        Text text = this.GetComponent<Text>();

        if(fadeaway)
        {
            for (float i = 1f; i >= 0; i -= Time.deltaTime / 1)
            {
                text.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i <= 1f; i += Time.deltaTime / 1)
            {
                text.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
