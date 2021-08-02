using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//�񼮰� ū�濡�� ���Ǵ� ���� �÷��̾�
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
        if (scene.name == "��" || scene.name == "ū��")
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
