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
    public HeartControl playerHeart;
    public RPGTalk rpgTalk;
    public int phaseCount;

    private bool enemyActed;
    private bool isReading;
    private GameObject[] enemyAtks;

    void Start()
    {
        playerHeart.gameObject.SetActive(true);
        playerHeart.SetHeart();
        state = BattleState.Start;
        phaseCount = 0;
    }

    void Update()
    {
        if (state == BattleState.Start)
        {
            playerHeart.GetComponent<PlayerHealth>().levelClear = false;
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
                rpgTalk.NewTalk("battle_start", "battle_end", rpgTalk.txtToParse);
                rpgTalk.OnEndTalk += EnemyTurn;
            }

            if (phaseCount == 3)
            {
                isReading = true;
                rpgTalk.NewTalk("success_start", "success_end", rpgTalk.txtToParse);
                rpgTalk.OnEndTalk += EnemyTurn;
            }
            if (phaseCount == 4)
            {
                isReading = true;
                rpgTalk.NewTalk("success_start", "success_end", rpgTalk.txtToParse);
                rpgTalk.OnEndTalk += EnemyTurn;
            }
            if (phaseCount == 5)
            {
                isReading = true;
                rpgTalk.NewTalk("win_start", "win_end", rpgTalk.txtToParse);
                rpgTalk.OnEndTalk += Won;
            }
        }
        else if (state == BattleState.EnemyTurn)
        {
            if (!enemyActed)
            {
                Instantiate(enemiesInBattle[0].EnemiesAttacks[0], Vector3.zero, Quaternion.identity);
                enemyAtks = GameObject.FindGameObjectsWithTag("Enemy");
                enemyActed = true;
            }
            else
            {
                if (playerHeart.GetComponent<PlayerHealth>().HP <= 0)
                {
                    state = BattleState.Lost;
                }
                else if (playerHeart.GetComponent<PlayerHealth>().levelClear)
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
            foreach (GameObject obj in enemyAtks)
            {
                Destroy(obj);
            }
            playerHeart.GetComponent<PlayerHealth>().HP = playerHeart.GetComponent<PlayerHealth>().maxHP;
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

    public void EnemyFinishedTurn()
    {
        foreach(GameObject obj in enemyAtks)
        {
            Destroy(obj);
        }
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
