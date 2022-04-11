using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    public GameObject exitMenu;
    public GameManager gameManager;
    public SceneTransition sceneTranstion;
    public Lane[] laneList;

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
        foreach (Lane lane in laneList)
        {
            lane.keyAvailable = false;
        }
    }
    public void ResumeGame()
    {
        gameManager.PlaySong();
        foreach (Lane lane in laneList)
        {
            lane.keyAvailable = true;
        }
    }
}
