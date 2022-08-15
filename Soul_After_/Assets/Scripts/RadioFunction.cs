using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class RadioFunction : MonoBehaviour
{
    public AudioClip[] Songs;

    private GameObject musicPlayer;

    private void Start()
    {
        musicPlayer = GameObject.Find("Music Player");
        SelectSong();
    }
    public void SelectSong()
    {      
        int SongSelection = DialogueLua.GetVariable("SongSelection").asInt;
        musicPlayer.GetComponent<AudioSource>().clip = Songs[SongSelection - 1];
        musicPlayer.GetComponent<AudioSource>().Play();
    }
}
