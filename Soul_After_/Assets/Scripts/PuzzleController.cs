using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private Transform emptySpace;
    [SerializeField] private TileRenderer[] tiles;
    private Vector3 emptySpaceStart;
    private int emptySpaceIndex = 11;
    // Start is called before the first frame update
    void Start()
    {
        emptySpaceStart = new Vector3(emptySpace.position.x, emptySpace.position.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                if (Vector2.Distance(emptySpace.position, hit.transform.position) < 4) //Change the distance value when receive the painting
                {
                    TileRenderer thisTile = hit.transform.GetComponent<TileRenderer>();
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    emptySpace.position = thisTile.targetPosition;
                    thisTile.targetPosition = lastEmptySpacePosition;
                    int tileIndex = findIndex(thisTile);
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                    GetComponent<AudioSource>().Play();
                }
            }
        }
        int correctTiles = 0;
        foreach (var a in tiles)
        {
            if (a != null)
            {
                if (a.inRightPlace)
                {
                    correctTiles++;
                }
            }
            if (correctTiles == tiles.Length - 1)
            {
                //Effect to show the whole picture for few seconds 
                //then transition back to certain scene.
                Debug.Log("You Finished!");
            }
        }
    }
    public int findIndex(TileRenderer tr)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == tr)
                {
                    return i;
                }
            }
        }
        return -1;
    }
    public void ResetPuzzle()
    {
        foreach (var a in tiles)
        {
            if (a != null)
            {
                a.targetPosition = a.startPosition;
                Debug.Log("Reset!");
            }
        }
        emptySpace.position = emptySpaceStart;
    }
}
