using UnityEngine;
using UnityEngine.UI;

public class GameSettingManager : MonoBehaviour
{
    public FloatValue curVol;

    public void EnterOptions()
    {
        bool isFS = Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen;
        string curTxt = isFS ? "창 모드" : "전체화면";
        this.transform.Find("Option Settings/Fullscreen Mode/Text").GetComponent<Text>().text = curTxt;
    }

    public void ToggleFullscreen()
    {
        bool isFS = Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen;
        string curTxt = isFS ? "창 모드" : "전체화면";

        Screen.fullScreenMode = isFS ? FullScreenMode.Windowed : FullScreenMode.ExclusiveFullScreen;
        this.transform.Find("Option Settings/Fullscreen Mode/Text").GetComponent<Text>().text = curTxt;

        if (PlayerPrefs.HasKey("Screenmanager Fullscreen mode_h3630240806"))
        {
            PlayerPrefs.SetInt("Screenmanager Fullscreen mode_h3630240806", (int)Screen.fullScreenMode);
        }
    }

    public void EnterVolCtr()
    {
        AudioListener.volume = curVol.initialValue * 1f;
        this.transform.Find("Sound Settings/Slider").GetComponent<Slider>().value = curVol.initialValue * 1f;
    }

    public void AdjustVol(float volLvl)
    {
        AudioListener.volume = volLvl * 1f;
        curVol.initialValue = volLvl;
    }

    public void BackToMenu()
    {
        AudioListener.volume = curVol.initialValue * 0.25f;
    }
}
