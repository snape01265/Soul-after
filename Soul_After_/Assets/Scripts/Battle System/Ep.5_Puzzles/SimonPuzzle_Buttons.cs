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
    public AudioSource PressSFX;

    private Light2D light2D;
    private bool pressed = false;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Manager.isInputable && collision.CompareTag("Player") && !pressed)
        {
            if (PressSFX)
                PressSFX.Play();
            pressed = true;
            StartCoroutine(Flashing());
            Manager.UserInput.Add((int)InputColor);
            Manager.CheckMatching(Manager.UserInput);
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
