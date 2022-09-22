using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestData
{
    public bool isActive;

    public int questId;
    public string questName;
    public string description;
    public string reward;

    public QuestGoal goal; //퀘스트의 목표 설정값을 가져와 설정함

    public void setData(int questId,string questName, string des, string reward,QuestGoal goal) //데이터를 세팅함 리워드는 수정예정 
    {
        this.questId = questId;
        this.questName = questName;
        description = des;
        this.reward = reward;
        this.goal = goal;
    }
   

}

