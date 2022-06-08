using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DialogueSystemFunction : MonoBehaviour
{
    private DialogueSystemController dialogueSystem;
    void Start()
    {
        dialogueSystem = GameObject.Find("DialogueSystem").GetComponent<DialogueSystemController>();
    }

    public void EndDialogue()
    {
        dialogueSystem.StopConversation();
    }
}
