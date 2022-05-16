using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text accuracyText, badText, goodText, perfectText, missText, rankText, totalComboText, totalScoreText;

    private bool bgChange = false;

    static int maxComboCount;
    static int count;
    static CatchController cc;
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
        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CatchController>();
        multiplierThresholds = multiplierThresholdsContainer;
    }
    void Update()
    {
        comboText.text = comboScore.ToString();
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
            sceneTransition.ChangeScene();
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
                cc.Heal(1);
                // if hp = 5, do nothing
            }
        }
        count += 1;
    }
    public static void Miss()
    {
        comboScore = 0; 
        currentMultiplier = 1;
        //cc.TakeDamage(1);
        missCount += 1;
        count += 1;
    }
    public void EndTrack()
    {
        resultScreen.SetActive(true);

        badText.text = badNoteCount.ToString() + "x";
        goodText.text = goodNoteCount.ToString() + "x";
        perfectText.text = perfectNoteCount.ToString() + "x";
        missText.text = missCount.ToString() + "x";

        float totalHit = badNoteCount + goodNoteCount + perfectNoteCount;
        float totalNotes = badNoteCount + goodNoteCount + perfectNoteCount + missCount;
        float percentAccuracy = (totalHit / totalNotes) * 100f;

        accuracyText.text = percentAccuracy.ToString("F") + "%";

        string rankValue = "F";

        if(percentAccuracy > 40)
        {
            rankValue = "D";
            if (percentAccuracy > 55)
            {
                rankValue = "C";
                if (percentAccuracy > 70)
                {
                    rankValue = "B";
                    if (percentAccuracy > 85)
                    {
                        rankValue = "A";
                        if (percentAccuracy == 100)
                        {
                            rankValue = "S";
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
