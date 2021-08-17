using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotManager : MonoBehaviour
{
    private void Awake()
    {
        GameObject.Find("Player").GetComponent<Player>().ispaused = true;
    }

    public void SaveInSlot(int slotNo)
    {
        bool result = GameObject.Find("GameSaveManager").GetComponent<GameSaveManager>().SaveFunc(slotNo);

        if (result)
            Debug.Log("저장 성공!");
        else
            Debug.Log("저장 실패!");

        gameObject.SetActive(false);
        Exit();
    }

    public void Exit()
    {
        GameObject.Find("Player").GetComponent<Player>().ispaused = false;
    }
}
