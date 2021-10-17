using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//기억의 향연으로 들어갈 때 예, 아니오 대화창을 뜨게 하는 스크립트
public class EntranceChoice : MonoBehaviour
{
    public RPGTalk rpgTalk;
    public GameObject sceneTransition;
    public BoolValue passed;
    // Start is called before the first frame update
    public void ChoiceMade()
    {
        rpgTalk.OnMadeChoice += OnMadeChoice;      
    }

    void OnMadeChoice(string questionId, int choiceID)
    {
        if (choiceID == 0)
        {
            rpgTalk.PlayNext();
            sceneTransition.GetComponent<SceneTransition>().ChangeScene();
            if (passed != null)
            {
                passed.initialValue = true;
            }
        }
    }
}
