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
        //슬아
        talkData.Add(1000, new string[] {"안녕하세요?", "후세에 오신 것을 환영합니다!"});
        //마이클
        talkData.Add(1100, new string[] {"뭔가?", "지금 바쁘니 방해하지 말라구" });
        //빈센트
        talkData.Add(1200, new string[] { "당신과 함께 일할 수 있다니!", "정말로 이 흥분을 주체하지 못하겠다구 브라더!", "하하하하하하!" }); 
        //토마스
        talkData.Add(1001, new string[] { "안녕하신가 제군!", "나의 이 아름다운 근육에 반한건가?", "적어도 3대 500을 찍어야 이런 몸을 가질 수 있지!", "하하하하하하하!" });
        //Professor
        talkData.Add(1002, new string[] { "'죽음에 직면했을 때 우리는 자신의 독자적인 존재 앞에 직면한다'", "이 말의 의미가 무엇이라고 생각합니까?" });
        //통계부서 사자 1,2,3
        talkData.Add(1003, new string[] { "환생률 73%... 전년 대비 증가율 5%...", "음?", "이봐 지금 바쁘니깐 말걸지 말라구"});
        talkData.Add(1004, new string[] { "그거 아세요?", "이곳에 오는 아동들이 1000명당 3.6명 정도 된다는걸요", "안타까운 것들.." });
        talkData.Add(1005, new string[] { "쿠쿠쿡...", "이 파일만 정리하면 밖으로 나가서 몰래 베이즈 정리를 더 공부할 수 있겠군..", "쿠쿠쿡..."});
        //자료기록실 사자
        talkData.Add(1006, new string[] { "이곳은 자료 기록실 이랍니다!" });
     //Animal NPC Talking
        //앵무새 1, 2, 3, 4
        talkData.Add(200, new string[] { ".....", "누구세용?" });
        talkData.Add(201, new string[] { ".....", "누구세용?" });
        talkData.Add(202, new string[] { ".....", "누구세용?" });
        talkData.Add(203, new string[] { ".....", "누구세용?" });
        //예카테리나 2세
        talkData.Add(100, new string[] { "혹시 그거 안다냥?", "내가 첫 위대한 고양이 예카테리나 1세의 후손이란 걸냥?", "몰랐다면 이제부터 알아서 머리를 조아리라냥!" });
        //그릴즈 베어
        talkData.Add(101, new string[] { "....", "혹시 비석 주변에서 내 아들을 본 적 있는곰?", "....", "없다곰?", "... 알겠곰.." });
        //곰식이
        talkData.Add(102, new string[] { "ㅎㅇ", "지금 내부 공사중이라서 못 들어감", "그리고 형 좀 못생겨서 들여다 보내주면 다들 기겁할듯 ㅋ", "그러니깐 다른데서 놀으셈", "ㅂㅂ2" });
        //Quest Talk 
        talkData.Add(10 + 200, new string[] { "...", "누구세용?", "....", "아~ 인사?", "환영합니다! 환영합니다~", "다음! 다음!" });
        talkData.Add(11 + 210, new string[] {"...", "누구세용?", "....", "아~", "이름! 이름 부탁!", "....", "되게 이상한 이름이네용?", "다음! 다음!" });
        talkData.Add(12 + 220, new string[] { "..." , "누구세용?", "....", "아~", "등록! 등록!", "다음! 다음!"});
        talkData.Add(13 + 230, new string[] { "옷을 성공적으로 입었다" });



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
