using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    public void EndGame()
    {
        GameObject.Find("UI").SetActive(false);
        GameObject.Find("Objects").SetActive(false);
        GameObject.Find("Player").GetComponent<Player>().CancelControl();
        gameObject.transform.Find("Cutscene Manager").gameObject.SetActive(true);
    }
}
