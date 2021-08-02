using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHandler : MonoBehaviour
{
    public QuestObject[] questObject;
    public bool[] questCompleted;
    // Start is called before the first frame update
    void Start()
    {
        questCompleted = new bool[questObject.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
