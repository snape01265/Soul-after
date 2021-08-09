using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//�񼮰� ū�濡�� ���Ǵ� ���� �÷��̾�
public class MusicPlayer : MonoBehaviour
{
    public CutsceneList bgm;

    private void Awake()
    {
        SetUpSingleton();
    }
    public void UpdateMusic(string scenetoload)
    {
        string scene = scenetoload;
        if (bgm.initialValue.Contains(scene))
        {
            DontDestroyOnLoad(this.gameObject);
            Debug.Log("Not Destroyed");
        }
        else
        {
            Destroy(this.gameObject);
            Debug.Log("Destroyed");
        }

    }
    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public IEnumerator MusicFade()
    {

    }
}
