using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public TextMeshProUGUI QuestText;
    public GameObject panel;

    public QuestData []qData; //데이터를 배열로 가져옴

    public QuestGoal goal;
 
    Dictionary<int, QuestData> QuestList; //데이터 배열을 키 값으로 구분하여 분류 (퀘스트 목록)  


    private void Start()
    {
        //퀘스트 1
        QuestList = new Dictionary<int, QuestData>();

        goal.goalType = QuestGoal.GoalType.FindOther; //퀘스트의 유형
        goal.requiredAmount = 1;

        qData[0].setData(0, "루나와 대화하기"
            ,"루나에게 다가가 마우스 왼쪽 클릭을 하여 대화하십시오"
            , "보상은 없습니다.", goal);
            
        QuestList.Add(0, qData[0]); //각각 키에 맞는 퀘스트를 설정해줌
                                    
        //퀘스트 2
        goal.goalType = QuestGoal.GoalType.Gathering;
        goal.requiredAmount = 10;

        qData[1].setData(1, "광석을 10개 모으시오"
            , "광석을 찾아 채광 하세요"
            , "보상은 없습니다.", goal);

        QuestList.Add(1, qData[1]); //각각 키에 맞는 퀘스트를 설정해줌
    }


    public void OpenQuestWindow()
    {
        panel.SetActive(true);
       // QuestText.text = qData.description;
    }
    public void CloseQuestWindow()
    {
        panel.SetActive(false);
    }
}
