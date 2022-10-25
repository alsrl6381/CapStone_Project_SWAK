using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("REFERENCE")]
    private QuestManager questManager;
    private Player player;
    private TalkManager talkManager;
    private GameObject scanObject;

    [Header("UI")]
    public GameObject talkPanel;
    public Text talkText;
    public Image portraitImg;
    public Text ItemLogText;
    public GameObject optionPannel;
    public GameObject savePannel;

    public static bool isAction;
    public int talkIndex;


    private void Start()
    {

        talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
        questManager = GameObject.Find("QuestInfo").GetComponent<QuestManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
        talkIndex = 0;
        
    }
    private void Update()
    {
        ShowOptionMenu();
    }

    public void Action(GameObject scanObj)
    {
        isAction = true;
        scanObject = scanObj;
        NPC_Data Data = scanObject.GetComponent<NPC_Data>();
        Talk(Data.NPC_ID, Data.isNpc);
        talkPanel.SetActive(isAction);
    }

    void Talk(int id,bool isNpc)
    {
        string talkData;

        if(talkManager.talkData.ContainsKey(id + QuestManager.MainQuestIndex))
        {
            talkData = talkManager.GetTalk(id + QuestManager.MainQuestIndex, talkIndex);
        }else
        {
            talkData = talkManager.GetTalk(id,talkIndex);
        }
       

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }
        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            //초상화때문에 오류 생김 나중에 수정
            portraitImg.sprite = talkManager.GetPortrait(id+QuestManager.MainQuestIndex, int.Parse(talkData.Split(':')[1])); //  : 을 기준으로 대화와 초상화를 구별함
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
           // Debug.Log
            
            portraitImg.color = new Color(1, 1, 1, 1);
            talkText.text = talkData;

        }
        isAction = true;
        talkIndex++;
    }
    public void ItemLog(string name,int count) //아이템을 얻거나 잃었을 때 뜨는 로그
    {
        string str;
        if (count > 0)
        {
            str = name + "을(를) " + count + "개 획득하였습니다.";
            ItemLogText.text = ItemLogText.text.Insert(ItemLogText.text.Length, str + "\n");               
        }
        else
        {
            str = name + "을(를) " + -count + "개 잃었습니다.";
            ItemLogText.text = ItemLogText.text.Insert(ItemLogText.text.Length, str + "\n");
        }
        StartCoroutine(DeleteLog(str));
    }
    IEnumerator DeleteLog(string size)
    {
        yield return new WaitForSeconds(4f);
        ItemLogText.text=ItemLogText.text.Remove(0,size.Length+1);
        
    }

    void ShowOptionMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionPannel.activeSelf)
            {
                savePannel.SetActive(false);
                optionPannel.SetActive(false);
            }

            optionPannel.SetActive(true);        
        }
    }
 

}
