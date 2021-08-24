using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver()
    {
        GameObject.Find("UI").SetActive(false);
        GameObject.Find("Objects").SetActive(false);
        GameObject.Find("Player").GetComponent<Player>().CancelControl();
        GameObject.Find("Scene")
    }
}
