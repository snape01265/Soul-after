using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//비석과 큰길에서 사용되는 뮤직 플레이어
public class MusicPlayer : MonoBehaviour
{
    Scene scene;
    private void Awake()
    {
        SetUpSingleton();
    }
    private void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "비석" || scene.name == "큰길")
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
}
