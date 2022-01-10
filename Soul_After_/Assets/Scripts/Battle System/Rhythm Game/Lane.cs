using Melanchall.DryWetMidi.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode keyToPress;
    public GameObject notePrefab;
    public GameManager gameManager;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();

    int spawnIndex = 0;
    int inputIndex = 0;
    int count = 0;

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, GameManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }

    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (GameManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - GameManager.instance.noteTime)
            {
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }
        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double perfectMarginOfError = GameManager.instance.perfectMarginOfError;
            double goodMarginOfError = GameManager.instance.goodMarginOfError;
            double badMarginOfError = GameManager.instance.badMarginOfError;
            double audioTime = GameManager.GetAudioSourceTime() - (GameManager.instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(keyToPress))
            {
                if (Mathf.Abs((float)(audioTime - timeStamp)) < perfectMarginOfError)
                {
                    ScoreManager.BadHit();
                    notes[inputIndex].gameObject.GetComponent<Note>().PerfectHit();
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                    count++;
                }
                else if (Mathf.Abs((float)(audioTime - timeStamp)) < goodMarginOfError)
                {
                    ScoreManager.GoodHit();
                    notes[inputIndex].gameObject.GetComponent<Note>().GoodHit();
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                    count++;
                }
                else if (Mathf.Abs((float)(audioTime - timeStamp)) < badMarginOfError)
                {
                    ScoreManager.PerfectHit();
                    notes[inputIndex].gameObject.GetComponent<Note>().BadHit();
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                    count++;
                }
                else
                {
                    Debug.Log("OutOfRange");
                }
            }
            if(timeStamp + badMarginOfError <= audioTime)
            {
                ScoreManager.Miss();
                print($"Missed");
                inputIndex++;
                count++;
            }
        }
        else if (timeStamps.Count == inputIndex)
        {
            gameManager.ChangeSong();
        }
    }
}
