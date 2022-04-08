using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    public GameObject exitMenu;
    public GameManager gameManager;
    public SceneTransition sceneTranstion;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (exitMenu.activeSelf)
            {
                ResumeGame();
                exitMenu.SetActive(false);
            }
            else
            {
                PauseGame();
                exitMenu.SetActive(true);
            }
        }
    } 
    public void ExitGame()
    {
        sceneTranstion.ChangeScene();
    }
    public void Continue()
    {
        ResumeGame();
        exitMenu.SetActive(false);
    }
    public void PauseGame()
    {
        gameManager.PauseSong();
    }
    public void ResumeGame()
    {
        gameManager.PlaySong();
    }
}
