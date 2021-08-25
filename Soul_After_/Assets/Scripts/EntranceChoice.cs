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
    void Start()
    {
        rpgTalk.OnMadeChoice += OnMadeChoice;
        this.gameObject.SetActive(!passed.initialValue);
    }

    void OnMadeChoice(string questionId, int choiceID)
    {
        if (passed.initialValue == false & choiceID == 0)
        {
            passed.initialValue = true;
            sceneTransition.SetActive(true);
        }
        else
        {
            sceneTransition.SetActive(false);
        }
    }
}
