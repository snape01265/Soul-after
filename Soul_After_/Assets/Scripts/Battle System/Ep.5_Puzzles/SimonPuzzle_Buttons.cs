using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SimonPuzzle_Buttons : MonoBehaviour
{
    public SimonPuzzleManager Manager;
    public SimonPuzzleManager.COLORS InputColor;
    public float FlashingDuration = .25f;
    public float BrightIntensity = .3f;
    public float DimIntensity = .05f;

    private Light2D light2D;
    private bool pressed = false;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !pressed)
        {
            pressed = true;
            StartCoroutine(Flashing());
            Manager.UserInput.Add((int)InputColor);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pressed = false;
    }

    IEnumerator Flashing()
    {
        light2D.intensity = DimIntensity;
        yield return new WaitForSeconds(FlashingDuration);
        light2D.intensity = BrightIntensity;
    }
}
