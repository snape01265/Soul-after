using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;
    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        questList.Add(0, new QuestData("Explorer", new int[] { 0 }));
        questList.Add(10, new QuestData("사자 등록하기", new int[] {200, 210, 220, 230}));
        questList.Add(20, new QuestData("퀘스트 클리어", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }
    public string CheckQuest()
    {
        //Quest Name
        return questList[questId].questName;
    }
    public string CheckQuest(int id)
    {
        //Next Talk Target
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        //Control Quest Object
        ControlObject();

        //Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        //Quest Name
        return questList[questId].questName;
    }
    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0; 
    }
    void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if(questActionIndex == 1)
                    questObject[0].SetActive(true);
                break;           
        }
    }
}
