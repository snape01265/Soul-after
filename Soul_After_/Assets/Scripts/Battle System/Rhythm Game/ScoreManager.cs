using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using UnityEngine.Playables;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public float bgChangeCount;
    public TMPro.TextMeshPro comboText;
    public TMPro.TextMeshPro scoreText;
    public int[] multiplierThresholdsContainer;
    [HideInInspector]
    public GameManager gameManager;
    public GameObject resultScreen;
    public SceneTransition sceneTransition;
    public PlayableDirector gameover;
    [HideInInspector]
    public Text accuracyText, badText, goodText, perfectText, missText, rankText, totalComboText, totalScoreText;

    private bool bgChange = false;

    static int maxComboCount;
    static int count;
    static int currentScore;
    static int comboScore;
    static int[] multiplierThresholds;
    static int currentMultiplier = 1;
    static int badNoteValue = 50;
    static int goodNoteValue = 100;
    static int perfectNoteValue = 300;
    static int badNoteCount;
    static int goodNoteCount;
    static int perfectNoteCount;
    static int missCount;

    void Start()
    {
        instance = this;
        multiplierThresholds = multiplierThresholdsContainer;
    }
    void Update()
    {
        comboText.text = "Combos " + comboScore.ToString();
        scoreText.text = currentScore.ToString();
        if (count == bgChangeCount && !bgChange)
        {
            gameManager.ChangeBG();
            bgChange = true;
        }
        if (GameManager.GetAudioSourceTime() > gameManager.tracks[GameManager.track].clip.length - 0.05 && !resultScreen.activeInHierarchy)
        {
            Invoke(nameof(EndTrack), 2f);
        }
        if (resultScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            string rankAchieved = DialogueLua.GetVariable("RhythmGame.RankValue").AsString;

            if (rankAchieved == "F" || rankAchieved == "D")
            {
                gameover.Play();
            }
            else
            {
                sceneTransition.ChangeScene();
            }
            bgChange = false;
            currentScore = 0;
            comboScore = 0;
            count = 0;
            maxComboCount = 0;
            perfectNoteCount = 0;
            goodNoteCount = 0;
            badNoteCount = 0;
            missCount = 0;
            currentMultiplier = 1;
        }
    }
    public static void PerfectHit()
    {
        currentScore += perfectNoteValue * currentMultiplier;
        perfectNoteCount += 1;
        NoteHit();
    }
    public static void GoodHit()
    {
        currentScore += goodNoteValue * currentMultiplier;
        goodNoteCount += 1;
        NoteHit();
    }
    public static void BadHit()
    {
        currentScore += badNoteValue * currentMultiplier;
        badNoteCount += 1;
        NoteHit();
    }
    public static void NoteHit()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            comboScore++;
            if(maxComboCount < comboScore)
            {
                maxComboCount = comboScore;
            }
            if (multiplierThresholds[currentMultiplier - 1] <= comboScore)
            {
                currentMultiplier++;
            }
        }
        count += 1;
    }
    public static void Miss()
    {
        comboScore = 0; 
        currentMultiplier = 1;
        missCount += 1;
        count += 1;
    }
    public void EndTrack()
    {
        gameManager.isPlaying = false;
        resultScreen.SetActive(true);

        badText.text = badNoteCount.ToString() + "x";
        goodText.text = goodNoteCount.ToString() + "x";
        perfectText.text = perfectNoteCount.ToString() + "x";
        missText.text = missCount.ToString() + "x";

        float totalHit = (badNoteCount * 50) + (goodNoteCount * 100) + (perfectNoteCount * 300);
        float totalNotes = (badNoteCount + goodNoteCount + perfectNoteCount + missCount) * 300;
        float percentAccuracy = (totalHit / totalNotes) * 100f;

        accuracyText.text = percentAccuracy.ToString("F") + "%";

        string rankValue = "F";
        DialogueLua.SetVariable("RhythmGame.RankValue", rankValue);

        if (percentAccuracy > 40)
        {
            rankValue = "D";
            DialogueLua.SetVariable("RhythmGame.RankValue", rankValue);
            if (percentAccuracy > 55)
            {
                rankValue = "C";
                DialogueLua.SetVariable("RhythmGame.RankValue", rankValue);
                if (percentAccuracy > 70)
                {
                    rankValue = "B";
                    DialogueLua.SetVariable("RhythmGame.RankValue", rankValue);
                    if (percentAccuracy > 85)
                    {
                        rankValue = "A";
                        DialogueLua.SetVariable("RhythmGame.RankValue", rankValue);
                        if (percentAccuracy == 100)
                        {
                            rankValue = "S";
                            DialogueLua.SetVariable("RhythmGame.RankValue", rankValue);
                        }
                    }
                }
            }
        }
        rankText.text = rankValue;
        totalScoreText.text = currentScore.ToString();
        totalComboText.text = maxComboCount.ToString() + "x";
        comboText.text = "";
        scoreText.text = "";
    }
}
