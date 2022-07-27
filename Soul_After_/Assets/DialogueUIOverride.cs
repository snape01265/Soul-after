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
            case "����":
                Image.sprite = DialoguePanels[1];
                break;

            case "��Ʈ":
                Image.sprite = DialoguePanels[2];
                break;

            case "����Ŭ":
                Image.sprite = DialoguePanels[3];
                break;

            case "�丶��":
                Image.sprite = DialoguePanels[4];
                break;

            case "��ī�׸��� 2��":
                Image.sprite = DialoguePanels[5];
                break;

            case "���� ���� ��":
                Image.sprite = DialoguePanels[6];
                break;

            case "����":
                Image.sprite = DialoguePanels[7];
                break;

            default:
                Image.sprite = DialoguePanels[0];
                break;
        }
    }
}
