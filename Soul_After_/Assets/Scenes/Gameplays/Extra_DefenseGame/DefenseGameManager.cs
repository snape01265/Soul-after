using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseGameManager : MonoBehaviour
{
    [Header("Objs")]
    public FloatValue HighScore;
    public MobGen[] MobGens;
    public Fadein FadeIn;
    public SceneTransition SceneTransition;
    public Text CurWaveText;
    public Text HighScoreText;
    public Text CurScoreText;
    [Header("Barrier Settings")]
    public int BarrierHealth = 100;
    public int BarrierHealPerRound = 15;
    [Header("Mob Settings")]
    public int EnemyAtkDmg = 7;
    public int EnemyHealth = 20;
    public float EnemyAtkTime = 2f;
    [Header("Gun Settings")]
    public float GunAtkSpd = .5f;
    public int GunAtkDmg = 5;
    [Header("Game Settings")]
    public int ClassicTotWave = 10;
    public int WaveEnemyMul = 10;
    public int WaveClearBreakTime = 5;
    public int GameEndFadeOutTime = 2;

    [HideInInspector]
    public int CurWave {
        get { 
            return (curWave);
        }
        set {
            curWave = value;
            RenderCurWave();
        }
    }
    private int curWave;
    [HideInInspector]
    public int CurScore
    {
        get
        {
            return (curScore);
        }
        set
        {
            curScore = value;
            RenderCurScore();
        }
    }
    private int curScore;

    private int waveKill;
    private bool isClassic = true;

    public void StartClassic()
    {
        InitScene();
        for (int i = 0; i < ClassicTotWave; i++)
        {
            StartWave(i * WaveEnemyMul);
        }

        ClassicEndGame();
    }

    public void StartEndless()
    {
        isClassic = false;
        InitScene();
        for (int i = 0; ; i++)
        {
            StartWave(i * WaveEnemyMul);
        }
    }

    private void StartWave(int MobCount)
    {
        int curMob = 0;
        waveKill = 0;

        while (true)
        {
            if (curMob < MobCount)
            {
                curMob++;
                SpawnMob();
            }

            if (waveKill >= MobCount)
            {
                Debug.Log("Wave Cleared");
                break;
            }
        }
    }

    private void SpawnMob()
    {
        int mobPos = Random.Range(0, 6);
        //spawn mob from random pos
    }

    private void ClassicEndGame()
    {
        // add token count
        EndGame();
    }

    private void EndGame()
    {
        if (HighScore.initialValue < CurScore)
        {
            HighScore.initialValue = CurScore;
        }

        FadeIn.FadeInOutStatic(GameEndFadeOutTime);
        SceneTransition.ChangeScene();
    }

    private void InitScene()
    {
        if (isClassic) return;
        else
        {
            HighScoreText.text = "현재 점수: " + ((int)HighScore.initialValue).ToString();
        }
    }

    private void RenderCurWave()
    {
        if (isClassic) return;
        else
        {
            CurWaveText.text = "웨이브: " + curWave.ToString();
        }
    }

    private void RenderCurScore()
    {
        if (isClassic) return;
        else
        {
            CurScoreText.text = "현재 점수: " + curScore.ToString();
        }
    }
}
