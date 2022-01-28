using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxPuzzleManager : MonoBehaviour
{
    private Transform playerLoc;

    private void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void MirrorActivate()
    {

    }
}
