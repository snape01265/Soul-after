using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PuzzleController : MonoBehaviour
{
    private float tileDistance;
    [SerializeField] private Transform emptySpace;
    [SerializeField] private TileRenderer[] tiles;
    public float lightChangeDuration;
    public float waitDuration;
    public bool puzzleFin;

    private Vector3 emptySpaceStart;
    private int emptySpaceIndex = 11;
    private Light2D myLight;
    private GameObject resetBtn;
    private string sceneName;
    // Start is called before the first frame update
    void Awake()
    {
        emptySpaceStart = new Vector3(emptySpace.position.x, emptySpace.position.y, 0f);
        myLight = GameObject.Find("Light Controller").GetComponent<Light2D>();
        resetBtn = GameObject.FindGameObjectWithTag("ResetBtn");
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Ep.1 Puzzle_1")
        {
            tileDistance = 3f;
        }
        else
        {
            tileDistance = 4f;
        }
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
                    if (Vector2.Distance(emptySpace.position, hit.transform.position) < tileDistance) //Change the distance value when receive the painting
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
                    StartCoroutine(lightInAndOut(myLight, lightChangeDuration, waitDuration));
                }
            }
        }
    }
    IEnumerator lightControl(Light2D lightToFade, bool fadeIn, float duration)
    {
        float minLuminosity = 0;
        float maxLuminosity = 5;
        float counter = 0f;

        float a, b;

        if (fadeIn)
        {
            a = minLuminosity;
            b = maxLuminosity;
        }
        else
        {
            a = maxLuminosity;
            b = minLuminosity;
        }

        float currentIntensity = lightToFade.intensity;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);

            yield return null;
        }
    }

    IEnumerator lightInAndOut(Light2D lightToFade, float duration, float waitTime)
    {
        WaitForSeconds waitSec = new WaitForSeconds(waitTime);

        yield return lightControl(lightToFade, true, duration);

        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("TileId"))
        {
            tile.SetActive(false);
        }
        GameObject.FindGameObjectWithTag("Finish").GetComponent<SpriteRenderer>().enabled = true;

        yield return lightControl(lightToFade, false, duration - 1f);

        yield return waitSec;

        GameObject.Find("Scene Transition").GetComponent<SceneTransition>().ChangeScene();
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
            }
        }
        emptySpace.position = emptySpaceStart;
        Debug.Log("Reset!");
    }
}
