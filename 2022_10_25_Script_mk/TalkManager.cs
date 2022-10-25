using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{ 
    public Dictionary<int, string[]> talkData; //혹시 사용할지도 모르니까 만들어둠
    public Dictionary<int ,Sprite> portraitData;
    public Sprite[] portraitArr;
    QuestManager quest;

    private void Start()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();

        quest = GameObject.Find("QuestInfo").GetComponent<QuestManager>();
        GenerateData();
    }

    void GenerateData()
    {
        //튜토리얼 관련 대화
        talkData.Add(0, new string[] { " 방향키를 이용하여 움직이시오. "}); //퀘스트 인덱스와 대화 내용 저장
        talkData.Add(1, new string[] { "송신탑에 마우스 왼쪽 클릭을 하여 수신을 받으십시오." });

        //메인 퀘스트 관련 대화
        talkData.Add(1000, new string[] { quest.QuestList[1000].description});
        
        //서브 퀘스트 관련 대화 
        talkData.Add(2000, new string[] { quest.QuestList[2000].description});
        talkData.Add(2001, new string[] { quest.QuestList[2001].description});
        talkData.Add(2002, new string[] { quest.QuestList[2002].description});

        talkData.Add(100, new string[] { "그냥 평범한 큐브인 것 같다....:0" });
        talkData.Add(1100, new string[] { "큐브를 조사했다...:0" ,"별로 특이한 점은 없는 것 같다. 송신탑으로 돌아가보자.:0"}); //뒤에 :0은 초상화를 구별하기 위한 표시
        

        talkData.Add(1300, new string[] { "이 물건은 스피어이다.:0" });

        portraitData.Add(100, portraitArr[0]);
        portraitData.Add(1000, portraitArr[0]);
        portraitData.Add(1100, portraitArr[0]); //해당 아이디에 초상화를 저장 만약 한 대화에 초상화가 여러개라면 id값에 1씩 더해서 뒤에 스프라이트를 다른 걸 넣으면 됨
       
        portraitData.Add(1300, portraitArr[0]); 


    }
    public string GetTalk(int id, int talkindex)
    {
        if (talkindex == talkData[id].Length)
        {
            if (quest.QuestList[QuestManager.MainQuestIndex].Check_ID[0]+ QuestManager.MainQuestIndex == id && quest.QuestList[QuestManager.MainQuestIndex].isAccept)
            {
                quest.QuestList[QuestManager.MainQuestIndex].goal.Find();
            }
            return null;
        }
        else
            return talkData[id][talkindex];
    }
    public Sprite GetPortrait(int id,int portraitindex)
    {
        return portraitData[id + portraitindex];
    }
}
