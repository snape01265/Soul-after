using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    private QuestHandler theQH;
    public int questNumber;
    public bool startQuest;
    public bool endQuest;
    // Start is called before the first frame update
    void Start()
    {
        theQH = FindObjectOfType<QuestHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            if (!theQH.questCompleted[questNumber])
            {
                if(startQuest && !theQH.questObject[questNumber].gameObject.activeSelf)
                {
                    theQH.questObject[questNumber].gameObject.SetActive(true);
                }
            }
            if(endQuest && theQH.questObject[questNumber].gameObject.activeSelf)
            {
                theQH.questObject[questNumber].EndQuest();
            }
        }
    }
}
