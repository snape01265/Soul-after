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
    public float SpawnRateDecreMulti = 1.1f; //Spawning multiplier lengthened for every Simul spawn
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
    private bool isSpawnable = false;
    private bool waveStarted = false;
    private bool spawnFinished = false;
    [NonSerialized]
    public bool isClassicMode = true;
    private bool gameEnded = false;
    private float spawnRate;
    private int simulSpawn;
    private int MobCount = 0;
    [NonSerialized]
    public int curMob = 0;
    private float spawnRateSimulDecre = 1; //Spawning multiplier lengthened for every Simul spawn
    private readonly int SPAWNRATEDECINCRE = 2; //Spawning multiplier applied for every x(3) stages 
    private readonly int[] SIMULSPAWNINCINCRE = { 5, 10, 20, 30 }; //Simultaneous spawn triggered every round in array
    private readonly int CLEARTXTBUFFERTIME = 1;

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
                        MobGens[i].Invoke("SpawnMob", UnityEngine.Random.value * spawnRate);
                    }
                }
                else
                {
                    for (int i = 0; i < MobGens.Length; i++)
                    {
                        curMob++;
                        MobGens[i].Invoke("SpawnMob", UnityEngine.Random.value * spawnRate);
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
                StartCoroutine(CheckWaveFinished());
            }
        }
    }

    public void StartClassic()
    {
        isClassicMode = true;
        InitScene();
        StartCoroutine(StartWave());
    }

    public void StartEndless()
    {
        isClassicMode = false;
        InitScene();
        CurScore = 0;
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        CurWave++;

        if (CurWave % SPAWNRATEDECINCRE == 0)
        {
            spawnRate *= SpawnMulti;
        }

        if (Array.Exists(SIMULSPAWNINCINCRE, x => x == CurWave))
        {
            simulSpawn += 1;
            spawnRateSimulDecre *= SpawnRateDecreMulti;
            spawnRate = BaseSpawnRate * spawnRateSimulDecre;
        }

        curMob = 0;
        MobCount = WaveEnemyMul * CurWave;
        WaveKill = 0;
        waveStarted = true;
        yield return null;
    }

    IEnumerator CheckWaveFinished()
    {
        if (isClassicMode && CurWave >= ClassicTotWave)
        {
            ClassicEndGame();
            yield break;
        } else if (!isClassicMode || CurWave < ClassicTotWave)
        {
            Barrier.RestoreHealth();
            StartCoroutine(RenderClearText());
            yield return new WaitForSeconds(WaveClearBreakTime);
            StartCoroutine(StartWave());
        }

    }

    private void ClassicEndGame()
    {
        // add token count
        EndGame();
    }

    public void EndGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;

            if (!isClassicMode && HighScore.initialValue < CurScore)
            {
                HighScore.initialValue = CurScore;
            }

            FadeIn.FadeInOutStatic(GameEndFadeOutTime);
            SceneTransition.Invoke("ChangeScene", GameEndFadeOutTime);
        }
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
