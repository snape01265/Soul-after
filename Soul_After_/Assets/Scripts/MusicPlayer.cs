using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//�񼮰� ū�濡�� ���Ǵ� ���� �÷��̾�
public class MusicPlayer : MonoBehaviour
{
    public string SceneToPlayMusic1;
    public string SceneToPlayMusic2;
    Scene scene;
    private void Awake()
    {
        SetUpSingleton();
    }
    private void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == SceneToPlayMusic1 || scene.name == SceneToPlayMusic2)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
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
