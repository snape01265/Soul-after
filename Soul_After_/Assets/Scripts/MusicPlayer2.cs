using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//인간부서 1층과 2층에서 사용되는 뮤직 플레이어
public class MusicPlayer2 : MonoBehaviour
{
    Scene scene;
    private void Awake()
    {
        SetUpSingleton();
    }
    private void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "인간부서1층" || scene.name == "인간부서2층")
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
