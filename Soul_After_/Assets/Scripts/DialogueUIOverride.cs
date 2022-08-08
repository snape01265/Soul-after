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
            case "����":
                image.sprite = dialoguePanels[1];
                image.color = new Color(.6f, .6f, .6f);
                break;

            case "��Ʈ":
                image.sprite = dialoguePanels[2];
                image.color = new Color(.6f, .6f, .6f);
                break;

            case "����Ŭ":
                image.sprite = dialoguePanels[3];
                image.color = new Color(.6f, .6f, .6f);
                break;

            case "�丶��":
                image.sprite = dialoguePanels[4];
                break;

            case "��ī�׸��� 2��":
                image.sprite = dialoguePanels[5];
                break;

            case "���� ���� ��":
                image.sprite = dialoguePanels[6];
                image.color = new Color(.6f, .6f, .6f);
                break;

            case "����":
                image.sprite = dialoguePanels[7];
                image.color = new Color(.6f, .6f, .6f);
                break;

            default:
                image.sprite = dialoguePanels[0];
                image.color = new Color(.8f, .8f, .8f);
                break;
        }
    }
}
