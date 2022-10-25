using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public Inventory inventory;

    public Dictionary<string, int> RequireItem;//퀘스트 달성에 필요한 아이템

    public bool IsMeet = false;

    public bool ItemCheck;

    public string[] RequireItemName; //Require 아이템 이름
  
    
    public bool IsReached() //각각의 퀘스트 타입마다의 달성 조건
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
    
    
    public void CheckQuestItem(string name) //퀘스트에 필요한 아이템 있는지 체크
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

    public void CheckQuestItem(string name1,string name2) //퀘스트에 필요한 아이템 있는지 체크
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
    