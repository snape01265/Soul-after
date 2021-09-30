using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchController : MonoBehaviour
{
    public Text scoreText;

    private int score;
    void Start()
    {
        
    }

    void Update()
    {
        scoreText.text = score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Flower")
        {
            Destroy(other.gameObject);
            score = score + 100;
        }
    }
}
