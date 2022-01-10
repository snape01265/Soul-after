using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using PixelCrushers.DialogueSystem;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public float songDelayInSeconds;
    public int inputDelayInMilliseconds;
    public double perfectMarginOfError;
    public double goodMarginOfError;
    public double badMarginOfError;
    public string fileLocation;
    public static MidiFile midiFile;
    public Transform player;
    public Transform seulha;
    public Image firstBG;
    public Image secondBG;
    public Image snowImage;

    public float noteTime;
    public float noteSpawnX;
    public float noteTapX;
    public float noteDespawnX
    {
        get
        {
            return noteTapX - (noteSpawnX - noteTapX);
        }
    }

    [System.Obsolete]
    void Start()
    {
        instance = this;
        if (Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFromWebsite());
        }
        else
        {
            ReadFromFile();
        }
    }

    [System.Obsolete]
    private IEnumerator ReadFromWebsite()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                using (var stream = new MemoryStream(results))
                {
                    midiFile = MidiFile.Read(stream);
                    GetDataFromMidi();
                }
            }
        }
    }
    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
    }
    public void StartRhythmGame()
    {
        GetDataFromMidi();
    }
    private void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        foreach (var lane in lanes) lane.SetTimeStamps(array);

        Invoke(nameof(StartSong), songDelayInSeconds);
    }
    public void StartSong()
    {
        audioSource.Play();
    }
    public void StartDialogue()
    {
        PixelCrushers.DialogueSystem.DialogueManager.StartConversation("Ep.2_RhythmGame_Conversations", player, seulha);
    }
    public void ChangeBG()
    {
        StartCoroutine(FadeTo(0.0f, 3.0f, firstBG));
        StartCoroutine(FadeTo(0.0f, 3.0f, snowImage));
        firstBG.enabled = false;
        snowImage.enabled = false;
        secondBG.enabled = true;
        StartCoroutine(FadeTo(1.0f, 3.0f, secondBG));
    }

    IEnumerator FadeTo(float alphaValue, float time, Image image)
    {
        float initialValue = image.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(initialValue, alphaValue, time));
            image.color = newColor;
            yield return new WaitForEndOfFrame();
        }
    }
    public static double GetAudioSourceTime()
    {
        return (double)instance.audioSource.timeSamples / instance.audioSource.clip.frequency;
    }
}
