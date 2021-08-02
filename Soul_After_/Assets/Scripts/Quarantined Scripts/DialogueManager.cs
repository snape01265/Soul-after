using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{/*
    public TalkManager talkManager;
    public QuestManager questManager; 
    public GameObject talkPanel;
    public Text talkText;
    public Text Name; 
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

    private void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjId objId = scanObject.GetComponent<ObjId>();
        Talk(objId.id, objId.isNpc);
        talkPanel.SetActive(isAction); 
    }

    public void Talk(int id, bool isNpc)
    {
        //Set Talk Data
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id+ questTalkIndex, talkIndex);
        //End Talk
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }
        if (isNpc)
        {
            Name.text = scanObject.name;
            talkText.text = talkData;
        }
        else
        {
            Name.text = scanObject.name;
            talkText.text = talkData; 
        }
        isAction = true;
        talkIndex++;
    }
    */
}
