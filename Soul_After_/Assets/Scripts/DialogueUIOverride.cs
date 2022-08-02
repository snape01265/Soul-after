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
        actorName = subtitle.speakerInfo.Name;
        ChangeDialogueUI();
    }
    public void ChangeDialogueUI()
    {
        image = GetComponent<Image>();
        switch (actorName)
        {
            case "슬아":
                image.sprite = dialoguePanels[1];
                break;

            case "빈센트":
                image.sprite = dialoguePanels[2];
                break;

            case "마이클":
                image.sprite = dialoguePanels[3];
                break;

            case "토마스":
                image.sprite = dialoguePanels[4];
                break;

            case "예카테리나 2세":
                image.sprite = dialoguePanels[5];
                break;

            case "여우 같은 놈":
                image.sprite = dialoguePanels[6];
                break;

            case "곰식":
                image.sprite = dialoguePanels[7];
                break;

            default:
                image.sprite = dialoguePanels[0];
                break;
        }
    }
}
