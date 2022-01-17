using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using PixelCrushers.DialogueSystem;
using UnityEngine.Experimental.Rendering.Universal;


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
    public float bgSpeed;

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
        IEnumerator BackgroundTransition()
        {
            Light2D sunlight = GameObject.Find("Sunlight").GetComponent<Light2D>();
            StartCoroutine(Fade(true, firstBG, bgSpeed));
            StartCoroutine(Fade(true, snowImage, bgSpeed));
            GameObject.Find("SnowParticles").SetActive(false);
            yield return new WaitForSeconds(5);
            StartCoroutine(Fade(false, secondBG, bgSpeed));
            StartCoroutine(FadeLight(false, sunlight, bgSpeed));
            yield return null;
        }
        StartCoroutine(BackgroundTransition());
    }

    IEnumerator Fade(bool fadeAway, Image image, float t)
    {
        if (fadeAway)
        {
            for (float i = 0.5f; i >= 0; i -= Time.deltaTime / t)
            {
                image.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i <= 0.5f; i += Time.deltaTime / t)
            {
                image.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
    IEnumerator FadeLight(bool fadeAway, Light2D light, float t)
    {
        if(fadeAway)
        {
            for (float i = 0.5f; i >= 0; i -= Time.deltaTime / t)
            {
                light.intensity = i;
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i <= 0.5f; i += Time.deltaTime / t)
            {
                light.intensity = i;
                yield return null;
            }
        }
        yield return null;
    }
    public static double GetAudioSourceTime()
    {
        return (double)instance.audioSource.timeSamples / instance.audioSource.clip.frequency;
    }
}
