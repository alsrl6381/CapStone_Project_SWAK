using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public TalkManager talkManager;
    public int talkindex=0;

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        Data_NPC data = scanObject.GetComponent<Data_NPC>();
        Talk(data.ID, data.isNPC);
    }
    
    void Talk(int Id, bool isNpc)
    {
      string talkdata =  talkManager.GetTalk(Id, talkindex);
        if(isNpc)
        {
            talkText.text = talkdata;
        }
        else
        {
            talkText.text = talkdata;
        }
    }
    
}
