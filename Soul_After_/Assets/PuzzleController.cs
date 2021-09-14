using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    private List<TileRenderer> tileRenderer;
    private GameObject[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        int[,] startState = new int[4, 3] { { 1, 2, 3 }, { 4, 8, 5 }, { 10, 7, 6 }, { 11, 9, -1 } };
        int[,] finishedState = new int[4, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 10, 11, -1 } };
        tiles = GameObject.FindGameObjectsWithTag("TileId");
        tileRenderer = new List<TileRenderer>();
        foreach (GameObject t in tiles)
        {
            TileRenderer _tileRend = t.GetComponent<TileRenderer>();
            int idx = _tileRend.tileId - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
