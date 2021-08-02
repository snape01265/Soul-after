using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartCutscene : MonoBehaviour
{
    public PlayableDirector playable;
    public void CutsceneStart()
    {
        playable.Play();
    }
}
