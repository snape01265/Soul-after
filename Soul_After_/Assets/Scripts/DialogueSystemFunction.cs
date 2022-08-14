using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class DialogueSystemFunction : MonoBehaviour
{
    public void EndDialogue()
    {
        if (DialogueManager.IsConversationActive)
        {
            GameObject.Find("Continue Button").GetComponent<Button>().interactable = true;
            DialogueManager.StopConversation();
            DialogueManager.Unpause();
        }    
    }
}
