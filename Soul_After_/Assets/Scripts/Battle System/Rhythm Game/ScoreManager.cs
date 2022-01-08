using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMPro.TextMeshPro comboText;
    public TMPro.TextMeshPro scoreText;
    public Player player;
    public int[] multiplierThresholdsContainer;

    static CatchController cc;
    static int currentScore;
    static int comboScore;
    static int[] multiplierThresholds;
    static int currentMultiplier = 1;
    static int badNoteValue = 300;
    static int goodNoteValue = 100;
    static int perfectNoteValue = 50;

    void Start()
    {
        instance = this;
        cc = player.GetComponent<CatchController>();
        multiplierThresholds = multiplierThresholdsContainer;
    }

    public static void PerfectHit()
    {
        currentScore += perfectNoteValue * currentMultiplier;
        NoteHit();
    }
    public static void GoodHit()
    {
        currentScore += goodNoteValue * currentMultiplier;
        NoteHit();
    }
    public static void BadHit()
    {
        currentScore += badNoteValue * currentMultiplier;
        NoteHit();
    }
    public static void NoteHit()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            comboScore++;
            if (multiplierThresholds[currentMultiplier - 1] <= comboScore)
            {
                currentMultiplier++;
                cc.GetFlower(1);
            }
        }
    }
    public static void Miss()
    {
        comboScore = 0; 
        currentMultiplier = 1;
        cc.TakeDamage(1);
    }
    void Update()
    {
        comboText.text = comboScore.ToString();
        scoreText.text = currentScore.ToString();
    }
}
