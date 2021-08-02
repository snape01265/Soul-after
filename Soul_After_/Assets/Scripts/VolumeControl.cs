using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour
{
    public AudioSource _audio; 
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    public void LowerVolume()
    {
        _audio.volume = 0.1f;
    }
    public void IncreaseVolume()
    {
        _audio.volume = 0.5f;
    }

}
