using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SimonPuzzleManager : MonoBehaviour
{
    public Player Player;
    public BoolList Progress;
    public Transform HubPos;
    public Transform StartPos;
    public Fadein Fadein;
    [Header("Puzzle Settings")]
    public int PuzzlePatterns = 9;
    public float FadeinDuration;
    public float PuzzleStartTime;
    public float PatternDuration;
    public float PatternBreakTime;
    public float LightsTime = 1;
    public float LightsTurnOff = 1;
    [HideInInspector]
    public List<int> UserInput
    {
        get
        {
            return userInput;
        }
        set
        {
            if (isInputable)
            {
                userInput = value;
                CheckMatching(value);
            }
        }
    }
    private List<int> userInput;
    [HideInInspector]
    public bool isInputable = false;

    private PlayerHealth health;
    private int currentStage;
    private int[] puzzleAnswer;
    
    private readonly int[] DIFFICULTIES = { 1, 1, 1, 2, 2, 2, 3, 3, 3 };
    private readonly int DMG = 1;

    private void Start()
    {
        health = Player.GetComponent<PlayerHealth>();
    }

    public void InitPuzzle()
    {
        currentStage = 0;
        StartCoroutine(StartPuzzle());
        health.RestoreHealth();
    }

    public void FinPuzzle()
    {
        Progress.initialValue[2] = true;
        StartCoroutine(TeletoHub());
    }

    // dif = 1 : Easy
    // dif = 2 : Medium
    // dif = 3 : Hard
    private int[] MakeAnswer(int dif)
    {
        List<int> ans = new List<int>();
        
        switch (dif)
        {
            case 1:
                {
                    for(int i = 0; i < 3; i++)
                        ans.Add(Random.Range(0, 3));
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < 4; i++)
                        ans.Add(Random.Range(0, 3));
                    break;
                }
            case 3:
                {
                    for (int i = 0; i < 5; i++)
                        ans.Add(Random.Range(0, 3));
                    break;
                }
        }

        return ans.ToArray();
    }

    private void CheckMatching(List<int> inputPat)
    {
        if (inputPat.Last() == puzzleAnswer[inputPat.Count - 1] && inputPat.Count == puzzleAnswer.Length)
        {
            StopCoroutine(StartPattern());
            StartCoroutine(TakeABreak());
        }
        else if (inputPat.Last() == puzzleAnswer[inputPat.Count - 1])
            return;
        else
        {
            health.TakeDamage(DMG);
            StopCoroutine(StartPattern());
            StartCoroutine(TakeABreak());
        }
    }

    IEnumerator StartPattern()
    {
        if (PuzzlePatterns - 1 < currentStage)
        {
            FinPuzzle();
            yield break;
        }

        puzzleAnswer = MakeAnswer(DIFFICULTIES[currentStage]);
        var renderLights = StartCoroutine(RenderLights(puzzleAnswer));
        while (renderLights != null)
        {
            yield return null;
        }

        currentStage++;
        isInputable = true;
        yield return new WaitForSeconds(PatternDuration);
        isInputable = false;
        health.TakeDamage(DMG);

        yield return new WaitForSeconds(PatternBreakTime);
        StartCoroutine(StartPattern());
    }

    IEnumerator StartPuzzle()
    {
        Fadein.FadeInOutStatic(FadeinDuration);
        yield return new WaitForSeconds(FadeinDuration / 2);
        Player.transform.position = StartPos.position;
        yield return null;
        yield return new WaitForSeconds(FadeinDuration / 2);
        yield return new WaitForSeconds(PuzzleStartTime);
        StartCoroutine(StartPattern());
    }

    IEnumerator RenderLights(int[] ans)
    {
        yield return new WaitForSeconds(5);
    }

    IEnumerator TeletoHub()
    {
        Fadein.FadeInOutStatic(FadeinDuration);
        yield return new WaitForSeconds(FadeinDuration / 2);
        Player.transform.position = HubPos.position;
        yield return null;
    }

    IEnumerator TakeABreak()
    {
        yield return new WaitForSeconds(PatternBreakTime);
        StartCoroutine(StartPattern());
    }
}
