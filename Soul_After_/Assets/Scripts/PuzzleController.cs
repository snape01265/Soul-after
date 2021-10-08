using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private Transform emptySpace;
    [SerializeField] private TileRenderer[] tiles;
    public float lightIntensity;
    public float speed;
    public bool puzzleFin;

    private Vector3 emptySpaceStart;
    private int emptySpaceIndex = 11;
    private Light2D myLight;
    private GameObject resetBtn;
    private int cases;

    // Start is called before the first frame update
    void Start()
    {
        emptySpaceStart = new Vector3(emptySpace.position.x, emptySpace.position.y, 0f);
        myLight = GameObject.Find("Light Controller").GetComponent<Light2D>();
        resetBtn = GameObject.FindGameObjectWithTag("ResetBtn");
    }

    // Update is called once per frame
    void Update()
    {
        if (!puzzleFin)
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
                    puzzleFin = true;
                    if (resetBtn != null)
                    {
                        resetBtn.SetActive(false);
                        //퍼즐 완료 Sound Effect 추가
                    }
                    StartCoroutine(Wait2Seconds());
                    cases = 1;
                }
            }
        }
        if (cases == 1 && puzzleFin)
        {
            myLight.intensity += speed * Mathf.Lerp(0, lightIntensity, Time.deltaTime);
            StartCoroutine(Wait4Seconds());
            foreach (GameObject tile in GameObject.FindGameObjectsWithTag("TileId"))
            {
                tile.SetActive(false);
            }
        }
        else if (cases == 2 && puzzleFin)
        {
            myLight.intensity -= 4 * speed * Mathf.Lerp(0, lightIntensity, Time.deltaTime);
            GameObject.FindGameObjectWithTag("Finish").GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(Wait2Seconds());
            cases = 3;
        }
        else if (cases == 3 && puzzleFin)
        {
            myLight.intensity = 0;
            GameObject.Find("Scene Transition").GetComponent<SceneTransition>().ChangeScene();
        }
    }
    IEnumerator Wait4Seconds()
    {
        yield return new WaitForSeconds(4);
        cases = 2;
    }
    IEnumerator Wait2Seconds()
    {
        yield return new WaitForSeconds(2);
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
