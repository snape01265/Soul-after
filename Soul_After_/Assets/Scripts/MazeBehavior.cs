using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeBehavior : MonoBehaviour
{
    public BoolList mazeState;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject Blocker1;
    public GameObject Blocker2;
    public GameObject Father1;
    public GameObject Father2;

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
        }
    }

    public void EnableMazeState(int idx)
    {
        if (!mazeState.defaultValue[idx])
            mazeState.defaultValue[idx] = true;
    }
}
