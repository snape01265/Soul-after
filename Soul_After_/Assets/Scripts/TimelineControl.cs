using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineControl : MonoBehaviour
{
    public float skipTime;

    private PlayableDirector playableDirector;
    private bool skippedTimeline = false;
    void Update()
    {
        if (!skippedTimeline)
        {
            if (Input.GetButtonDown("Jump"))
            {
                SkipToPoint(skipTime);
            }
        } 
    }

    private void SkipToPoint(float designatedTime)
    {
        playableDirector = GetComponent<PlayableDirector>();
        playableDirector.time = designatedTime;
        skippedTimeline = true;
    }
}
