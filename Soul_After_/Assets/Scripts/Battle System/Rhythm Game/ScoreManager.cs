using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using UnityEngine.Playables;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMPro.TextMeshPro comboText;
    public TMPro.TextMeshPro scoreText;
    public int[] multiplierThresholdsContainer;
    [HideInInspector]
    public float bgChangeCount;
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public GameObject resultScreen;
    [HideInInspector]
    public SceneTransition sceneTransition;
    [HideInInspector]
    public PlayableDirector gameover;
    [HideInInspector]
    public Animator textAnimation;
    [HideInInspector]
    public Text accuracyText, badText, goodText, perfectText, missText, rankText, totalComboText, totalScoreText;

    private bool bgChange = false;

    static int maxComboCount;
    static int count;
    static int currentScore;
    static int comboScore = 1;
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
        scoreText.text = "Score\n" + currentScore.ToString() + "\nx" + currentMultiplier.ToString();
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
            comboScore = 1;
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
        instance.comboText = GameObject.Find("ComboCounter").GetComponent<TMPro.TextMeshPro>();
        instance.comboText.text = comboScore.ToString() + "\nPerfect";
        instance.comboText.color = new Color(1f, 0.7960784f, 0.4470588f, 1);
        currentScore += perfectNoteValue * currentMultiplier;
        perfectNoteCount += 1;
        NoteHit();
    }
    public static void GoodHit()
    {
        instance.comboText = GameObject.Find("ComboCounter").GetComponent<TMPro.TextMeshPro>();
        instance.comboText.text = comboScore.ToString() + "\nGood";
        instance.comboText.color = new Color(0.4705882f, 0.8862745f, 0.8862745f, 1);
        currentScore += goodNoteValue * currentMultiplier;
        goodNoteCount += 1;
        NoteHit();
    }
    public static void BadHit()
    {
        instance.comboText = GameObject.Find("ComboCounter").GetComponent<TMPro.TextMeshPro>();
        instance.comboText.text = comboScore.ToString() + "\nBad";
        instance.comboText.color = new Color(0.5882353f, 0.8862745f, 0.4705882f, 1);
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
            if (multiplierThresholds[currentMultiplier - 1] <= comboScore - 1)
            {
                currentMultiplier++;
            }
        }
        comboAnimation();
        count += 1;
    }
    public static void Miss()
    {
        comboScore = 1; 
        currentMultiplier = 1;
        missCount += 1;
        count += 1;
    }
    public static void comboAnimation()
    {
        instance.textAnimation = GameObject.Find("ComboCounter").GetComponent<Animator>();
        instance.textAnimation.SetTrigger("Hit");
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
