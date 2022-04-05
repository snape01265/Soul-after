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
    public float SpawnBorderYUpper;
    public float SpawnBorderYLower;
    [Header("Gun Settings")]
    public float GunAtkSpd = .5f;
    public int GunAtkDmg = 5;
    public float MaxGunSpreadDeg = 30f;
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
    [HideInInspector]
    public int WaveKill;
    [HideInInspector]
    public bool OutForBlood = false;
    private bool isClassic = true;
    private float spawnRate;
    private int simulSpawn;
    private readonly int SPAWNRATEDECINCRE = 5;
    private readonly int SIMULSPAWNINCINCRE = 30;
    private readonly int CLEARTXTBUFFERTIME = 1;

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
        for (int i = 1; ; i++)
        {
            if (i % SPAWNRATEDECINCRE == 0)
            {
                spawnRate *= SpawnMulti;

                if (i % SIMULSPAWNINCINCRE == 0)
                {
                    simulSpawn += 1;
                }
            }
            
            StartWave(i * WaveEnemyMul);
        }
    }

    private void StartWave(int MobCount)
    {
        int curMob = 0;
        WaveKill = 0;
        bool isSpawnable = true;
        bool spawnFinished = false;

        while (true)
        {
            if (isSpawnable && curMob < MobCount)
            {
                if (simulSpawn <= 6)
                {
                    StartCoroutine(SpawnTimer());

                    for (int i = 0; i < simulSpawn; i++)
                    {
                        curMob++;
                        MobGens[i].SpawnMob();
                    }
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        curMob++;
                        MobGens[i].SpawnMob();
                    }
                }
            } else if (!spawnFinished && curMob > MobCount)
            {
                spawnFinished = true;
            }

            if (spawnFinished && WaveKill >= MobCount)
            {
                Barrier.RestoreHealth();
                StartCoroutine(RenderClearText())
                break;
            }
        }

        IEnumerator SpawnTimer()
        {
            isSpawnable = false;
            yield return new WaitForSeconds(spawnRate);
            isSpawnable = true;
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

        if (isClassic) return;
        else
        {
            HighScoreText.text = "최고 점수: " + ((int)HighScore.initialValue).ToString();
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

    IEnumerator RenderClearText()
    {
        yield return new WaitForSeconds(CLEARTXTBUFFERTIME);
        ClearText.gameObject.SetActive(true);
        yield return new WaitForSeconds(WaveClearBreakTime - CLEARTXTBUFFERTIME * 2);
        ClearText.gameObject.SetActive(false);
    }
}
