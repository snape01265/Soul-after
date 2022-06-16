using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyReference : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameController").GetComponent<GameManager>();
        if(gameManager.isPlaying)
        {
        }
    }
}
