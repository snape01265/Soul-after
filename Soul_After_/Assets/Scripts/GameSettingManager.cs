using UnityEngine;
using UnityEngine.UI;

public class GameSettingManager : MonoBehaviour
{
    private void Awake()
    {
        bool isFS = Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen;
        string curTxt = isFS ? "창 모드" : "전체화면" ;
        this.transform.Find("Text").GetComponent<Text>().text = curTxt;
    }

    public void ToggleFullscreen()
    {
        bool isFS = Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen;
        string curTxt = isFS ?  "창 모드" : "전체화면";

        Screen.fullScreenMode = isFS ? FullScreenMode.Windowed : FullScreenMode.ExclusiveFullScreen;
        this.transform.Find("Text").GetComponent<Text>().text = curTxt;
    }

    public void AdjustVol(float volLvl)
    {
        AudioListener.volume = volLvl;
    }
}
