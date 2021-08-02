using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData; 
    // Start is called before the first frame update
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData(); 
    }

    // Update is called once per frame
    void GenerateData()
    {
        //Talk Data
     //Human NPC Talking
        //����
        talkData.Add(1000, new string[] {"�ȳ��ϼ���?", "�ļ��� ���� ���� ȯ���մϴ�!"});
        //����Ŭ
        talkData.Add(1100, new string[] {"����?", "���� �ٻڴ� �������� ����" });
        //��Ʈ
        talkData.Add(1200, new string[] { "��Ű� �Բ� ���� �� �ִٴ�!", "������ �� ����� ��ü���� ���ϰڴٱ� ����!", "������������!" }); 
        //�丶��
        talkData.Add(1001, new string[] { "�ȳ��ϽŰ� ����!", "���� �� �Ƹ��ٿ� ������ ���Ѱǰ�?", "��� 3�� 500�� ���� �̷� ���� ���� �� ����!", "��������������!" });
        //Professor
        talkData.Add(1002, new string[] { "'������ �������� �� �츮�� �ڽ��� �������� ���� �տ� �����Ѵ�'", "�� ���� �ǹ̰� �����̶�� �����մϱ�?" });
        //���μ� ���� 1,2,3
        talkData.Add(1003, new string[] { "ȯ���� 73%... ���� ��� ������ 5%...", "��?", "�̺� ���� �ٻڴϱ� ������ ����"});
        talkData.Add(1004, new string[] { "�װ� �Ƽ���?", "�̰��� ���� �Ƶ����� 1000��� 3.6�� ���� �ȴٴ°ɿ�", "��Ÿ��� �͵�.." });
        talkData.Add(1005, new string[] { "������...", "�� ���ϸ� �����ϸ� ������ ������ ���� ������ ������ �� ������ �� �ְڱ�..", "������..."});
        //�ڷ��Ͻ� ����
        talkData.Add(1006, new string[] { "�̰��� �ڷ� ��Ͻ� �̶��ϴ�!" });
     //Animal NPC Talking
        //�޹��� 1, 2, 3, 4
        talkData.Add(200, new string[] { ".....", "��������?" });
        talkData.Add(201, new string[] { ".....", "��������?" });
        talkData.Add(202, new string[] { ".....", "��������?" });
        talkData.Add(203, new string[] { ".....", "��������?" });
        //��ī�׸��� 2��
        talkData.Add(100, new string[] { "Ȥ�� �װ� �ȴٳ�?", "���� ù ������ ����� ��ī�׸��� 1���� �ļ��̶� �ɳ�?", "�����ٸ� �������� �˾Ƽ� �Ӹ��� ���Ƹ����!" });
        //�׸��� ����
        talkData.Add(101, new string[] { "....", "Ȥ�� �� �ֺ����� �� �Ƶ��� �� �� �ִ°�?", "....", "���ٰ�?", "... �˰ڰ�.." });
        //������
        talkData.Add(102, new string[] { "����", "���� ���� �������̶� �� ��", "�׸��� �� �� �����ܼ� �鿩�� �����ָ� �ٵ� ����ҵ� ��", "�׷��ϱ� �ٸ����� ������", "����2" });
        //Quest Talk 
        talkData.Add(10 + 200, new string[] { "...", "��������?", "....", "��~ �λ�?", "ȯ���մϴ�! ȯ���մϴ�~", "����! ����!" });
        talkData.Add(11 + 210, new string[] {"...", "��������?", "....", "��~", "�̸�! �̸� ��Ź!", "....", "�ǰ� �̻��� �̸��̳׿�?", "����! ����!" });
        talkData.Add(12 + 220, new string[] { "..." , "��������?", "....", "��~", "���! ���!", "����! ����!"});
        talkData.Add(13 + 230, new string[] { "���� ���������� �Ծ���" });



    }
    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                return GetTalk(id - id % 100, talkIndex);//Get The Original Talk
            }
            else
            {
                return GetTalk(id - id % 10, talkIndex);// Get First Quest Talk
            }
        }
        if(talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
        
    }
}
