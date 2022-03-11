using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SimonPuzzle_Lightbulb : MonoBehaviour
{
    public SimonPuzzleManager Manager;
    private SpriteRenderer sprite;
    private Light2D light2D;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        light2D = GetComponent<Light2D>();

        light2D.enabled = false;
    }

    public void TurnOnToColor(int color)
    {
        light2D.enabled = true;
        switch (color)
        {
            case (int)SimonPuzzleManager.COLORS.Red:
                sprite.color = Color.red;
                break;
            case (int)SimonPuzzleManager.COLORS.Green:
                sprite.color = Color.green;
                break;
            case (int)SimonPuzzleManager.COLORS.Blue:
                sprite.color = Color.blue;
                break;
            case (int)SimonPuzzleManager.COLORS.Yellow:
                sprite.color = Color.yellow;
                break;
        }
    }

    public void TurnOff()
    {
        light2D.enabled = false;
        sprite.color = Color.black;
    }
}
