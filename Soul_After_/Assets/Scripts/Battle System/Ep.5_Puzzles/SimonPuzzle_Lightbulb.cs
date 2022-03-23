using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SimonPuzzle_Lightbulb : MonoBehaviour
{
    public SimonPuzzleManager Manager;
    private SpriteRenderer sprite;
    private Light2D light2D;
    private readonly Color32 RED = new Color32(255, 0, 0, 255);
    private readonly Color32 GREEN = new Color32(0, 255, 16, 255);
    private readonly Color32 BLUE = new Color32(0, 30, 255, 255);
    private readonly Color32 YELLOW = new Color32(255, 253, 0, 255);

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
                light2D.intensity = 2;
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
        light2D.intensity = 1;
        //sprite.color = Color.white;
    }
}
