using System;
using UnityEngine;
using UnityEngine.Playables;
[Serializable]
public class DialogueTriggerBehaviour : PlayableBehaviour
{/*
    public DialogueManager dialogueManager;
    public GameObject DialogueBox;
    public int id;
    public bool isNpc;
    public bool JumpToEnd = false;
    public string story;

    private PlayableGraph graph;
    private Playable thisPlayable; 
    private bool began = false;

    public override void OnPlayableCreate(Playable playable)
    {
        graph = playable.GetGraph(); 
        thisPlayable = playable; 
        DialogueManager dialogueManager = new DialogueManager();
        began = false;
    }
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (story != null)
        {
            graph.GetRootPlayable(0).SetSpeed(0);
            began = true;
            DialogueBox.SetActive(began);
            dialogueManager.Talk(id, isNpc);
        }
        else
        {
            if (JumpToEnd) 
                JumpToEndofPlayable();
            graph.GetRootPlayable(0).SetSpeed(1);
        }
    }
    public void OnDialogueEnd(object sender, EventArgs args)
    {
        graph.GetRootPlayable(0).SetSpeed(1);
        if (JumpToEnd) 
            JumpToEndofPlayable();
    }
    private void JumpToEndofPlayable() 
    { 
        graph.GetRootPlayable(0).SetTime(graph.GetRootPlayable(0).GetTime() + thisPlayable.GetDuration());
    }*/
}
