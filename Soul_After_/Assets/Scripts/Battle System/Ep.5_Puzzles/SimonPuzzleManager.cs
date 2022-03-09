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
    public SelectionDoorManager SelectionDoorManager;
    public Fadein Fadein;
    [Header("Puzzle Settings")]
    public int PuzzlePatterns = 9;
    public float FadeinDuration;
    public float PuzzleStartTime;
    public float PatternDuration;
    public float PatternBreakTime;
    [Header("Lightbulb Settings")]
    public SimonPuzzle_Lightbulb[] Lightbulbs;
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

    public enum COLORS
    {
        Red     = 0,
        Green   = 1,
        Blue    = 2,
        Yellow  = 3
    }
    private enum DIF
    {
        Easy    = 1,
        Medium  = 2,
        Hard    = 3
    }
    private enum ANSLENGTH
    {
        Easy = 3,
        Medium = 4,
        Hard = 5
    }

    private readonly int[] DIFFICULTIES = {
                                            (int)DIF.Easy, (int)DIF.Easy, (int)DIF.Easy,
                                            (int)DIF.Medium, (int)DIF.Medium, (int)DIF.Medium,
                                            (int)DIF.Hard, (int)DIF.Hard, (int)DIF.Hard
                                          };
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
        SelectionDoorManager.TrackProgress();
    }

    private int[] MakeAnswer(int dif)
    {
        List<int> ans = new List<int>();
        
        switch (dif)
        {
            case 1:
                {
                    for(int i = 0; i < (int)ANSLENGTH.Easy; i++)
                        ans.Add(Random.Range((int)COLORS.Red, (int)COLORS.Yellow));
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < (int)ANSLENGTH.Medium; i++)
                        ans.Add(Random.Range((int)COLORS.Red, (int)COLORS.Yellow));
                    break;
                }
            case 3:
                {
                    for (int i = 0; i < (int)ANSLENGTH.Hard; i++)
                        ans.Add(Random.Range((int)COLORS.Red, (int)COLORS.Yellow));
                    break;
                }
        }

        return ans.ToArray();
    }

    private void CheckMatching(List<int> inputPat)
    {
        if (inputPat == new List<int>())
            return;

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
        UserInput = new List<int>();
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
        yield return new WaitForSeconds(FadeinDuration / 2);
        yield return new WaitForSeconds(PuzzleStartTime);
        StartCoroutine(StartPattern());
    }

    IEnumerator RenderLights(int[] ans)
    {
        int idx = 0;
        foreach(int color in ans)
        {
            Lightbulbs[idx].TurnOnToColor(color);
            yield return new WaitForSeconds(LightsTime);
            idx++;
        }
        yield return new WaitForSeconds(LightsTurnOff);
        foreach (SimonPuzzle_Lightbulb lightbulb in Lightbulbs)
        {
            lightbulb.TurnOff();
        }
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
