using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class DialogueUIOverride : MonoBehaviour
{
    public Sprite[] dialoguePanels;

    private string actorName;
    private Image image;

    public void OnConversationLine(Subtitle subtitle)
    {
        ChangeDialogueUI();
    }
    public void ChangeDialogueUI()
    {
        image = GetComponent<Image>();
        actorName = DialogueManager.currentActor.ToString();
        switch (actorName)
        {
            case "Seulha (UnityEngine.Transform)":
                image.sprite = dialoguePanels[1];
                break;

            case "Vincent (UnityEngine.Transform)":
                image.sprite = dialoguePanels[2];
                break;

            case "Michael (UnityEngine.Transform)":
                image.sprite = dialoguePanels[3];
                break;

            case "Thomas (UnityEngine.Transform)":
                image.sprite = dialoguePanels[4];
                break;

            case "Katherina_II (UnityEngine.Transform)":
                image.sprite = dialoguePanels[5];
                break;

            case "Fox (UnityEngine.Transform)":
                image.sprite = dialoguePanels[6];
                break;

            case "GomShik (UnityEngine.Transform)":
                image.sprite = dialoguePanels[7];
                break;

            default:
                image.sprite = dialoguePanels[0];
                break;
        }
    }
}
