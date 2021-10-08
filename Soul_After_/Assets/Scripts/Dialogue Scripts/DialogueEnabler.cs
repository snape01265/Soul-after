using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEnabler : MonoBehaviour
{
    public List<BoolValue> episodeList = new List<BoolValue>();
    public List<RPGTalkArea> RPGTalks = new List<RPGTalkArea>();
    void Start()
    {
        ActivateObject();
    }
    public void ActivateObject()
    {
        int i = 0;
        foreach (BoolValue episode in episodeList)
        {
            if (episode.initialValue && RPGTalks[i + 1] != null)
            {
                RPGTalks[i].GetComponent<RPGTalkArea>().enabled = false;
                RPGTalks[i + 1].GetComponent<RPGTalkArea>().enabled = true;
                i++;
            }
        }
        RPGTalks[i].GetComponent<RPGTalkArea>().enabled = true;
    }
}
