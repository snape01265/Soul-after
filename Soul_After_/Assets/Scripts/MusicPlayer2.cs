using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//�ΰ��μ� 1���� 2������ ���Ǵ� ���� �÷��̾�
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
        if (scene.name == "�ΰ��μ�1��" || scene.name == "�ΰ��μ�2��")
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
