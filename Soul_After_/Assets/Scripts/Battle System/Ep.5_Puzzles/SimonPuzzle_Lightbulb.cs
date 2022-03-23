using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SimonPuzzle_Lightbulb : MonoBehaviour
{
    public SimonPuzzleManager Manager;
    private SpriteRenderer sprite;
    private Light2D light2D;
    private readonly Color RED = new Color(255f, 0f, 0f);
    private readonly Color GREEN = new Color(0f, 255f, 16f);
    private readonly Color BLUE = new Color(0f, 30f, 255f);
    private readonly Color YELLOW = new Color(255f, 253f, 0f);

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        light2D = GetComponent<Light2D>();

        light2D.enabled = false;
    }

    public void TurnOnToColor(int color)
    {
        switch (color)
        {
            case (int)SimonPuzzleManager.COLORS.Red:
                //sprite.color = RED;
                light2D.color = RED;
                break;
            case (int)SimonPuzzleManager.COLORS.Green:
                //sprite.color = GREEN;
                light2D.color = GREEN;
                break;
            case (int)SimonPuzzleManager.COLORS.Blue:
                //sprite.color = BLUE;
                light2D.color = BLUE;
                break;
            case (int)SimonPuzzleManager.COLORS.Yellow:
                //sprite.color = YELLOW;
                light2D.color= YELLOW;
                break;
        }
        light2D.enabled = true;
    }

    public void TurnOff()
    {
        light2D.enabled = false;
        //sprite.color = Color.white;
    }
}
