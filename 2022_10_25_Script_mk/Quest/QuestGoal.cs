using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public Inventory inventory;

    public Dictionary<string, int> RequireItem;//����Ʈ �޼��� �ʿ��� ������

    public bool IsMeet = false;

    public bool ItemCheck;

    public string[] RequireItemName; //Require ������ �̸�
  
    
    public bool IsReached() //������ ����Ʈ Ÿ�Ը����� �޼� ����
    {
        

        if (goalType == GoalType.FindOther)
        {
            return IsMeet;
        }
        else if (goalType == GoalType.GatheringItem)
        {
            return ItemCheck;
        }      
        else 
            return false;
        }
    
    
    public void CheckQuestItem(string name) //����Ʈ�� �ʿ��� ������ �ִ��� üũ
    {
        if (goalType == GoalType.GatheringItem)
        {
            if (RequireItem.ContainsKey(name))
            {
                if (inventory.ReturnItemCount(name) >= RequireItem[name])
                {
                    ItemCheck = true;
                }
            }
        }
        else ItemCheck = false;
    }

    public void CheckQuestItem(string name1,string name2) //����Ʈ�� �ʿ��� ������ �ִ��� üũ
    {
        Debug.Log("sda");
        if (goalType == GoalType.GatheringItem)
        {
            if ((inventory.ReturnItemCount(name2) >= RequireItem[name2]) && (inventory.ReturnItemCount(name1) >= RequireItem[name1]))
            {
                ItemCheck = true;;
            }
            
            else ItemCheck = false;
        }
    }
    
    public void Find()
    {
        if (goalType == GoalType.FindOther)
        {
            IsMeet = true;
        }
    }

    public enum GoalType
    {
        GatheringItem = 10,
        FindOther = 20
    }
   
}
    