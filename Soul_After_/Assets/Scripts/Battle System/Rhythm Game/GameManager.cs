using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.Experimental.Rendering.Universal;
using PixelCrushers.DialogueSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource[] tracks;
    public string[] fileLocation;
    public Lane[] lanes;
    public GameObject[] images;
    public float songDelayInSeconds;
    public int inputDelayInMilliseconds;
    public static MidiFile midiFile;
    public static int track;
    public float noteTime;
    public float noteSpawnY;
    public float noteTapY;
    //[HideInInspector]
    public double perfectMarginOfError;
    //[HideInInspector]
    public double goodMarginOfError;
    //[HideInInspector]
    public double badMarginOfError;
    //[HideInInspector]
    public double missMarginOfError;
    [HideInInspector]
    public ScoreManager scoreManager;
    public bool isMinigame;
    [HideInInspector]
    public bool isPlaying = false;

    private Transform player;
    private Transform seulha;
    private float bgSpeed = 10;
    public float noteDespawnY
    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY);
        }
    }

    void Start()
    {
        instance = this;
        track = DialogueLua.GetVariable("TrackSelection").asInt;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if(GameObject.FindGameObjectWithTag("NPC") != null)
        {
            seulha = GameObject.FindGameObjectWithTag("NPC").GetComponent<Transform>();
        }
        ReadFromFile();
        if (isMinigame)
        {
            StartRhythmGame();
        }
    }
    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation[track]);
    }
    public void StartRhythmGame()
    {
        if (!isMinigame && !isPlaying)
        {
            IEnumerator GameScreenTransition()
            {
                yield return new WaitForSeconds(1.5f);
                player.gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().SetPositionAndRotation(new Vector3(0, 0, -10), this.transform.rotation);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 7;
                yield return new WaitForSeconds(2);
                DialogueManager.StartConversation("Ep.2 Timelines/RhythmGame", player, seulha);
            }
            StartCoroutine(GameScreenTransition());
        }
        else
        {
            GetDataFromMidi();
            SetBG();
        }
    }
    public void GetDataFromMidi()
    {
        isPlaying = true;
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);
        foreach (var lane in lanes) lane.SetTimeStamps(array);
        Invoke(nameof(PlaySong), songDelayInSeconds);
    }
    public void PlaySong()
    {
        tracks[track].Play();
    }
    public void PauseSong()
    {
        tracks[track].Pause();
    }
    public void StartDialogue()
    {
        DialogueManager.StartConversation("Ep.2 Timeline 8(Rhythm Game)", player, seulha);
    }
    public void ChangeBG()
    {
        IEnumerator BackgroundTransition()
        {
            Light2D sunlight = GameObject.Find("Sunlight").GetComponent<Light2D>();
            StartCoroutine(Fade(true, images[0], bgSpeed));
            StartCoroutine(Fade(true, images[1], bgSpeed));
            StartCoroutine(Fade(true, GameObject.Find("��"), bgSpeed));
            yield return new WaitForSeconds(5);
            StartCoroutine(Fade(false, images[2], bgSpeed));
            StartCoroutine(FadeLight(false, sunlight, bgSpeed));
            yield return null;
        }
        StartCoroutine(BackgroundTransition());
    }
    public void SetBG()
    {
        images[track].SetActive(true);
    }
    IEnumerator Fade(bool fadeAway, GameObject image, float t)
    {
        SpriteRenderer imageComp = image.GetComponent<SpriteRenderer>();
        if (fadeAway)
        {
            for (float i = 0.5f; i >= 0; i -= Time.deltaTime / t)
            {
                imageComp.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i <= 0.5f; i += Time.deltaTime / t)
            {
                imageComp.color = new Color(1, 1, 1, i);
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
        return (double)instance.tracks[track].timeSamples / instance.tracks[track].clip.frequency;
    }
}
