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
