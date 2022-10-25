using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ClickQuestList : MonoBehaviour
{
    public int QuestID=0; //퀘스트 아이디를 담아오는 변수
    public Button btn;
    public QuestManager questManager;
    public static int ChoiceButtonId; //스태틱으로 선언하여 수락,거절,완료 버튼 누를시에 사용
    public static bool LoadingText;

    private void Start()
    {
        btn = gameObject.GetComponent<Button>();
        questManager = GameObject.Find("QuestInfo").GetComponent<QuestManager>();
    }

    public void setId(int QuestID) //해당 버튼이나 리스트에 퀘스트에 아이디를 지정해줌
    {
        this.QuestID = QuestID;
    }
    public void ClickItem() //퀘스트 리스트를 눌렀을 시 퀘스트 아이디에 해당하는 설명을 보여줌
    {
        if (QuestID != 0 && !LoadingText)
        {            
            questManager.QuestShowDescription(QuestID);
            ChoiceButtonId = QuestID;
        }
    }
    public void AcceptClick() //수락버튼 눌렀을 때 작동
    {
        questManager.QuestReceiver(ChoiceButtonId,true);
    }
    public void Complete() //퀘스트 완료 버튼 누를 시 작동
    {
        questManager.Complete(ChoiceButtonId);
        questManager.setQuestList();
    }
    public void RefuseClick() {
        questManager.QuestReceiver(ChoiceButtonId,false);


    }
}
