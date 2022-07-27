using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class DialogueUIOverride : MonoBehaviour
{
    public Sprite[] DialoguePanels;

    private Image Image;
    private Subtitle Subtitle;

    private void Update()
    {
        ChangeDialogueUI();
    }

    public void ChangeDialogueUI()
    {
        Image = GetComponent<Image>();
        string ActorName = Subtitle.speakerInfo.Name;

        switch (ActorName)
        {
            case "슬아":
                Image.sprite = DialoguePanels[1];
                break;

            case "빈센트":
                Image.sprite = DialoguePanels[2];
                break;

            case "마이클":
                Image.sprite = DialoguePanels[3];
                break;

            case "토마스":
                Image.sprite = DialoguePanels[4];
                break;

            case "예카테리나 2세":
                Image.sprite = DialoguePanels[5];
                break;

            case "여우 같은 놈":
                Image.sprite = DialoguePanels[6];
                break;

            case "곰식":
                Image.sprite = DialoguePanels[7];
                break;

            default:
                Image.sprite = DialoguePanels[0];
                break;
        }
    }
}
