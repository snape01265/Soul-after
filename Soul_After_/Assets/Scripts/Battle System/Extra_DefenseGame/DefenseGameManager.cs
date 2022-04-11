using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseGameManager : MonoBehaviour
{
    [Header("Objs")]
    public FloatValue HighScore;
    public MobGen[] MobGens;
    public Barrier Barrier;
    public Fadein FadeIn;
    public SceneTransition SceneTransition;
    public Text CurWaveText;
    public Text HighScoreText;
    public Text CurScoreText;
    public Text ClearText;
    [Header("Barrier Settings")]
    public int BarrierHealth = 100;
    public int BarrierHealPerRound = 15;
    [Header("Mob Settings")]
    public int EnemyAtkDmg = 7;
    public int EnemyHealth = 20;
    public float EnemySpd = 0.1f;
    public float EnemyAtkTime = 2f;
    public float BaseSpawnRate = 3f;
    public float SpawnMulti = .8f;
    public int BaseSimulSpawn = 1;
    public float SpawnBorderYUpper = 10f;
    public float SpawnBorderYLower = -10f;
    [Header("Gun Settings")]
    public float GunAtkSpd = .5f;
    public int GunAtkDmg = 5;
    public float MaxGunSpreadDeg = 10f;
    public float MaxGunSpreadTime = 3f;
    public float BulletSpd = 1f;
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
    [NonSerialized]
    public int WaveKill;
    public bool OutForBlood = false;
    private bool waveFinished = false;
    private bool isSpawnable = false;
    private bool waveStarted = false;
    private bool spawnFinished = false;
    private bool isClassicMode = true;
    private float spawnRate;
    private int simulSpawn;
    private int MobCount = 0;
    public int curMob = 0;
    private readonly int SPAWNRATEDECINCRE = 5;
    private readonly int SIMULSPAWNINCINCRE = 30;
    private readonly int CLEARTXTBUFFERTIME = 1;

    //test only
    private void Start()
    {
        Invoke("StartClassic", 3f);
    }

    private void FixedUpdate()
    {
        if (waveStarted)
        {
            if (isSpawnable && curMob < MobCount)
            {
                isSpawnable = false;
                if (simulSpawn <= MobGens.Length)
                {
                    for (int i = 0; i < simulSpawn; i++)
                    {
                        curMob++;
                        MobGens[i].SpawnMob();
                    }
                }
                else
                {
                    for (int i = 0; i < MobGens.Length; i++)
                    {
                        curMob++;
                        MobGens[i].SpawnMob();
                    }
                }
            }
            else if (!spawnFinished && curMob <= MobCount)
            {
                spawnFinished = true;
            }

            if (spawnFinished && WaveKill >= MobCount)
            {
                waveStarted = false;
                spawnFinished = false;
                StartCoroutine(CheckWaveFinished(isClassicMode));
            }
        }
    }

    public void StartClassic()
    {
        InitScene();
        StartCoroutine(StartWave(true));
    }

    public void StartEndless()
    {
        InitScene();
        StartCoroutine(StartWave(false));
    }

    IEnumerator StartWave(bool isClassic)
    {
        CurWave++;

        if (!isClassic && CurWave % SPAWNRATEDECINCRE == 0)
        {
            spawnRate *= SpawnMulti;

            if (CurWave % SIMULSPAWNINCINCRE == 0)
            {
                simulSpawn += 1;
            }
        }
        isClassicMode = isClassic;
        curMob = 0;
        MobCount = WaveEnemyMul * CurWave;
        WaveKill = 0;
        waveStarted = true;
        Debug.Log("curwave : " + CurWave);
        yield return null;
    }

    IEnumerator CheckWaveFinished(bool ClassicMode)
    {
        if (ClassicMode && CurWave >= ClassicTotWave)
        {
            ClassicEndGame();
            yield break;
        } else if (ClassicMode && CurWave < ClassicTotWave)
        {
            Barrier.RestoreHealth();
            StartCoroutine(RenderClearText());
            yield return new WaitForSeconds(WaveClearBreakTime);
            StartCoroutine(StartWave(ClassicMode));
        }

    }

    private void ClassicEndGame()
    {
        // add token count
        EndGame();
    }

    public void EndGame()
    {
        if (HighScore.initialValue < CurScore)
        {
            HighScore.initialValue = CurScore;
        }

        FadeIn.FadeInOutStatic(GameEndFadeOutTime);
        Debug.Log("Game Ended!");
        Time.timeScale = 0f;
    }

    private void InitScene()
    {
        spawnRate = BaseSpawnRate;
        simulSpawn = BaseSimulSpawn;

        StartCoroutine(SpawnTimer());

        if (isClassicMode) return;
        else
        {
            HighScoreText.text = "최고 점수: " + ((int)HighScore.initialValue).ToString();
        }
    }

    private void RenderCurWave()
    {
        CurWaveText.text = "웨이브: " + curWave.ToString();
    }

    private void RenderCurScore()
    {
        if (isClassicMode) return;
        else
        {
            CurScoreText.text = "현재 점수: " + curScore.ToString();
        }
    }

    IEnumerator RenderClearText()
    {
        yield return new WaitForSeconds(CLEARTXTBUFFERTIME);
        ClearText.gameObject.SetActive(true);
        yield return new WaitForSeconds(WaveClearBreakTime - CLEARTXTBUFFERTIME * 2);
        ClearText.gameObject.SetActive(false);
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnRate);
        isSpawnable = true;
        StartCoroutine(SpawnTimer());
    }
}
