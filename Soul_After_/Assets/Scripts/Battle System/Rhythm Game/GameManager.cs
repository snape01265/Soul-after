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
    public Player player;
    private CatchController cc;

    public int currentScore;
    public int normalNoteValue;
    public int goodNoteValue;
    public int perfectNoteValue;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public Text scoreText;
    public Text comboText;

    void Start()
    {
        instance = this;
        scoreText.text = "Score: " + currentScore;
        cc = player.GetComponent<CatchController>();
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
        else
        {
            if(!audioSource.isPlaying)
            {
                //대화 진행
            }
        }
    }

    public void NoteHit()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                currentMultiplier++;
                cc.GetFlower(1);
            }
            
        }
        comboText.text = "Combo: " + multiplierTracker;
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += normalNoteValue * currentMultiplier;
        NoteHit();
    }
    public void GoodHit()
    {
        currentScore += goodNoteValue * currentMultiplier;
        NoteHit();
    }
    public void PerfectHit()
    {
        currentScore += perfectNoteValue * currentMultiplier;
        NoteHit();
    }
    public void NoteMissed()
    {
        Debug.Log("Note missed");
        currentMultiplier = 1;
        multiplierTracker = 0;
        cc.TakeDamage(1);
        comboText.text = "Combo: " + multiplierTracker;
    }
}
