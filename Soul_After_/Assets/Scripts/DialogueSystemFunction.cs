using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class DialogueSystemFunction : MonoBehaviour
{
    public void EndDialogue()
    {
        if (DialogueManager.isConversationActive)
        {
            GameObject.Find("Continue Button").GetComponent<Button>().interactable = true;
        }
        DialogueManager.Unpause();
        DialogueManager.StopConversation();
    }

    public void EndDialogueForSavePoints()
    {
        DialogueManager.StopConversation();
    }
}
