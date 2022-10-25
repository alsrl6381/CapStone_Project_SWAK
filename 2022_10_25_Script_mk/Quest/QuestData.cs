using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestData
{ 
    public bool isActive; //����Ʈ�� ���� ���� �� ��Ȳ�̸� true
    public bool isAccept; //����Ʈ�� ������ true
    public bool isClear; //�Ϸ� �ߴ��� �Ǻ�

    public string questName; //����Ʈ �̸�
    public string description; //����
    public string reward; //����
    public int[] Check_ID; //����Ʈ�� �ִ� NPC�� �Ϸ��� �� NPC �����ϱ�
    public bool isMain; //���� ����Ʈ���� �ƴ��� ����

    public QuestGoal goal; //����Ʈ�� ��ǥ �������� ������ ������
    public GameManager gm;


    public void setData(string questName, string des, string reward, int[] Check,bool _Flag, QuestGoal goal) //�����͸� ������ ������� �������� 
    {
        isActive = true;
        this.questName = questName;
        description = des;
        this.reward = reward;
        this.goal = goal;
        Check_ID = Check;
        isMain = _Flag;
    }
    public void setData(string questName, string des, string reward, bool _Flag, QuestGoal goal) //�����͸� ������ ������� �������� 
    {
        isActive = true;
        this.questName = questName;
        description = des;
        this.reward = reward;
        this.goal = goal;
        isMain = _Flag;
    }
    public void setData(bool _IsActive,bool _IsAccept,bool _IsClaer) //�����͸� ������ ������� �������� 
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
        //�ε��� ������ ��� �ؾ� ���� ��� �غ��� �� ��
    }
   
}

