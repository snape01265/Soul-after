using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingManager : MonoBehaviour
{
    private Resolution[] resolutions;

    private void Awake()
    {
        resolutions = Screen.resolutions;
    }
    public void ToggleFullscreen()
    {
        Screen.fullScreenMode = Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen ? FullScreenMode.Windowed : FullScreenMode.ExclusiveFullScreen;
        Debug.Log("fullScreenMode is : " + Screen.fullScreenMode.ToString());
    }
}
