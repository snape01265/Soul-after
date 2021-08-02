using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayer : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject controlPanel;

    private void Director_Played(PlayableDirector obj)
    {
        controlPanel.SetActive(false);
    }
    private void Director_Stopped(PlayableDirector obj)
    {
        controlPanel.SetActive(true);
    }
    public void StartTimeline()
    {
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
    public void PauseTimeline()
    {
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
}
