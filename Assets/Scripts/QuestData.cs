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

    public QuestGoal goal; //����Ʈ�� ��ǥ �������� ������ ������

    public void setData(int questId,string questName, string des, string reward,QuestGoal goal) //�����͸� ������ ������� �������� 
    {
        this.questId = questId;
        this.questName = questName;
        description = des;
        this.reward = reward;
        this.goal = goal;
    }
   

}

