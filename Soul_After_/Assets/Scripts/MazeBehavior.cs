using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class MazeBehavior : MonoBehaviour
{
    public BoolList mazeState;
    public PlayerHealth playerHealth;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject Blocker1;
    public GameObject Blocker2;
    public GameObject Father1;
    public GameObject Father2;
    public GameObject BadMemory1;
    public GameObject BadMemory2;
    public GameObject BadMemory3;
    public GameObject GoodMemory1;
    public GameObject GoodMemory2;

    private void Start()
    {
        if (mazeState.initialValue[0])
        {
            Door1.GetComponent<SpriteFadeInOut>().SpriteFadeIn();
            Father1.SetActive(false);
        }
        if (mazeState.initialValue[1])
        {
            Door1.SetActive(false);
            Blocker1.SetActive(false);
            BadMemory1.SetActive(false);
            GoodMemory1.SetActive(true);
        }
        if (mazeState.initialValue[2])
        {
            Door2.GetComponent<SpriteFadeInOut>().SpriteFadeIn();
            Father2.SetActive(false);
        }
        if (mazeState.initialValue[3])
        {
            Door2.SetActive(false);
            Blocker2.SetActive(false);
            BadMemory2.SetActive(false);
            BadMemory3.SetActive(false);
            GoodMemory2.SetActive(true);
        }
        if (mazeState.initialValue.All<bool>(x => x == false))
        {
            playerHealth.RestoreHealth();
        }
    }

    public void EnableMazeState(int idx)
    {
        if (!mazeState.initialValue[idx])
            mazeState.initialValue[idx] = true;
    }
}
