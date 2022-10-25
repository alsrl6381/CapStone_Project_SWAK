using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestData
{ 
    public bool isActive; //퀘스트를 진행 가능 한 상황이면 true
    public bool isAccept; //퀘스트를 받으면 true
    public bool isClear; //완료 했는지 판별

    public string questName; //퀘스트 이름
    public string description; //설명
    public string reward; //보상
    public int[] Check_ID; //퀘스트를 주는 NPC와 완료할 때 NPC 구별하기
    public bool isMain; //메인 퀘스트인지 아닌지 구별

    public QuestGoal goal; //퀘스트의 목표 설정값을 가져와 설정함
    public GameManager gm;


    public void setData(string questName, string des, string reward, int[] Check,bool _Flag, QuestGoal goal) //데이터를 세팅함 리워드는 수정예정 
    {
        isActive = true;
        this.questName = questName;
        description = des;
        this.reward = reward;
        this.goal = goal;
        Check_ID = Check;
        isMain = _Flag;
    }
    public void setData(string questName, string des, string reward, bool _Flag, QuestGoal goal) //데이터를 세팅함 리워드는 수정예정 
    {
        isActive = true;
        this.questName = questName;
        description = des;
        this.reward = reward;
        this.goal = goal;
        isMain = _Flag;
    }
    public void setData(bool _IsActive,bool _IsAccept,bool _IsClaer) //데이터를 세팅함 리워드는 수정예정 
    {
        isActive = _IsActive;
        isAccept = _IsAccept;
        isClear = _IsClaer;
       
    }

    public void Complete()
    {
        if(goal.goalType == QuestGoal.GoalType.GatheringItem)
        {
            if(goal.RequireItemName.Length == 1) { 
            goal.inventory.GiveQuestItem(goal.RequireItemName[0], goal.RequireItem[goal.RequireItemName[0]]);
            }
            if (goal.RequireItemName.Length == 2)
            {
                goal.inventory.GiveQuestItem(goal.RequireItemName[0], goal.RequireItem[goal.RequireItemName[0]]);
                goal.inventory.GiveQuestItem(goal.RequireItemName[1], goal.RequireItem[goal.RequireItemName[1]]);
            }
        }
        isActive = false;
        isAccept = false;
        isClear = true;

        if (isMain)
            QuestManager.MainQuestIndex += 100;
        //인덱스 증가를 어떻게 해야 할지 고민 해봐야 할 듯
    }
   
}

