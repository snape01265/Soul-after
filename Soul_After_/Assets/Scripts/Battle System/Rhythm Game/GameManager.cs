using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;
    public bool startPlaying;
    public NoteSpeed noteSpeed;
    public static GameManager instance;
    public int currentScore;
    public int scoreValue = 2;
    public int currentCombo;
    public int comboTracker;
    public int[] comboThresholds;

    public Text scoreText;
    public Text comboText;

    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        currentCombo = 1;
    }

    void Update()
    {
        if(!startPlaying)
        {
            if(Input.anyKeyDown)
            {
                startPlaying = true;
                noteSpeed.hasStarted = true;

                audioSource.Play();
            }
        }
        
    }

    public void NoteHit()
    {
        Debug.Log("Hit");

        if (currentCombo - 1 < comboThresholds.Length)
        {
            comboTracker++;

            if (comboThresholds[currentCombo - 1] <= comboTracker)
            {
                comboTracker = 0;
                currentCombo++;
            }
        }
        comboText.text = "Combo: x" + currentCombo;
        currentScore += scoreValue * currentCombo;
        scoreText.text = "Score: " + currentScore;
    }

    public void NoteMissed()
    {
        Debug.Log("Note missed");

        currentCombo = 1;
        comboTracker = 0;

        comboText.text = "Combo: x" + currentCombo;
    }
}
