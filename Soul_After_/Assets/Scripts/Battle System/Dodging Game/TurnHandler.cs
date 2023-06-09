using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    FinishedTurn,
    Won,
    Lost
}
public class TurnHandler : MonoBehaviour
{
    public BattleState state;
    public EnemyProfile[] enemiesInBattle;
    public PlayerHealth playerHealth;
    public RPGTalk rpgTalk;
    public int phaseCount;
    public bool enemyActed;
    public int patternCount;
    private Animator anim;

    private bool isReading;
    private GameObject enemyAtk;

    void Start()
    {
        anim = GameObject.Find("�ƹ��� Older").GetComponent<Animator>();
        anim.SetBool("Battle", true);
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inBattle = true;
        state = BattleState.Start;
        phaseCount = 0;
    }

    void Update()
    {
        if (state == BattleState.Start)
        {
            phaseCount += 1;
            state = BattleState.PlayerTurn;
        }
        else if (state == BattleState.PlayerTurn && !isReading)
        {
            if(phaseCount == 1)
            {
                isReading = true;
                rpgTalk.NewTalk("tutorial_start", "tutorial_end", rpgTalk.txtToParse);
                rpgTalk.OnEndTalk += EnemyTurn;
            }

            if (phaseCount == 2)
            {
                isReading = true;
                rpgTalk.NewTalk("success1_start", "success1_end", rpgTalk.txtToParse);
                rpgTalk.OnEndTalk += EnemyTurn;
            }

            if (phaseCount == 3)
            {
                isReading = true;
                rpgTalk.NewTalk("success2_start", "success2_end", rpgTalk.txtToParse);
                rpgTalk.OnEndTalk += EnemyTurn;
            }
            if (phaseCount == 4)
            {
                isReading = true;
                rpgTalk.NewTalk("success3_start", "success3_end", rpgTalk.txtToParse);
                rpgTalk.OnEndTalk += EnemyTurn;
            }
            if (phaseCount == 5)
            {
                isReading = true;
                rpgTalk.NewTalk("success4_start", "success4_end", rpgTalk.txtToParse);
                anim.SetBool("Battle", false);
                GameObject.Find("AddCutscene").GetComponent<CutsceneEnter>().AddCutscene();
                rpgTalk.OnEndTalk += Won;           
            }
        }
        else if (state == BattleState.EnemyTurn)
        {
            if (!enemyActed)
            {
                Instantiate(enemiesInBattle[0].EnemiesAttacks[patternCount], Vector3.zero, Quaternion.identity);
                enemyAtk = GameObject.FindGameObjectWithTag("Enemy");
                enemyActed = true;
            }
            else
            {
                if (playerHealth.CurHP.initialValue <= 0)
                {
                    state = BattleState.Lost;
                }
                else if (enemyAtk.GetComponent<EnemyTurnHandler>().FinishedTurn)
                {
                    EnemyFinishedTurn();
                }
            }
        }
        else if (state == BattleState.FinishedTurn)
        {
            state = BattleState.Start;
        }
        else if (state == BattleState.Lost && !isReading)
        {
            Destroy(enemyAtk);
            playerHealth.CurHP.initialValue = playerHealth.maxHP;
            enemyActed = false;
            isReading = true;
            rpgTalk.NewTalk("fail_start", "fail_end", rpgTalk.txtToParse);
            rpgTalk.OnEndTalk += EnemyTurn;
        }
        else if (state == BattleState.Won && !isReading)
        {
            isReading = true;
            GameObject.Find("Scene Transition").GetComponent<SceneTransition>().ChangeScene();
        }
    }

    public void PlayerAct()
    {
        PlayerFinishTurn();
    }

    void PlayerFinishTurn()
    {
        state = BattleState.EnemyTurn;
    }

    public void PatternFinished()
    {
        Destroy(enemyAtk);
        enemyActed = false;
        patternCount += 1;
    }
    public void EnemyFinishedTurn()
    {
        Destroy(enemyAtk);
        enemyActed = false;
        state = BattleState.FinishedTurn;
    }
    void EnemyTurn()
    {
        state = BattleState.EnemyTurn;
        isReading = false;
    }
    void Won()
    {
        state = BattleState.Won;
        isReading = false;
    }
    public void Lost()
    {
        state = BattleState.Lost;
        isReading = false;
    }
}
