using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMPro.TextMeshPro comboText;
    public TMPro.TextMeshPro scoreText;
    public int[] multiplierThresholdsContainer;
    [HideInInspector]
    public GameManager gameManager;
    private bool bgChange = false;

    static int count;
    static int maxCount = 383;
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
        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CatchController>();
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
                cc.Heal(1);
                Debug.Log("1HP healed");
                // if hp = 5, do nothing
            }
        }
        count += 1;
        Debug.Log(count);
    }
    public static void Miss()
    {
        comboScore = 0; 
        currentMultiplier = 1;
        cc.TakeDamage(1);
        count += 1;
        Debug.Log(count);
    }
    void Update()
    {
        comboText.text = comboScore.ToString();
        scoreText.text = currentScore.ToString();
        if (count == 115 && !bgChange)
        {
            gameManager.ChangeBG();
            bgChange = true;
        }
        if (count == maxCount)
        {
            gameManager.StartDialogue();
        }
    }
}
