using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//����� �⿬���� �� �� ��, �ƴϿ� ��ȭâ�� �߰� �ϴ� ��ũ��Ʈ
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
