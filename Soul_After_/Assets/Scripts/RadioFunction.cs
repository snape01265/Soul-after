using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class RadioFunction : MonoBehaviour
{
    public AudioClip[] Songs;

    private AudioSource AudioSource;

    private void Awake()
    {
        SelectSong();
    }

    public void SelectSong()
    {
        AudioSource = GetComponent<AudioSource>();
        int SongSelection = DialogueLua.GetVariable("SongSelection").asInt;
        AudioSource.clip = Songs[SongSelection - 1];
        AudioSource.Play();
    }
}
