using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [Header("UI_QUEST")]
    public TalkManager Talk;
    public TypingEffect MessageEffect; //메시지 천천히 나오게 하는 기능
    public GameObject QuestMenuUI; //퀘스트 전체적인 UI
    public GameObject QuestListUi; //퀘스트 목록 보여주기
    public GameObject InProgressQuestUI; // 플레이어가 퀘스트 창 눌렀을 진행중인 퀘스트를 보여줄 UI
    public GameObject btn_panel_Choice; //퀘스트 수락 거절 완료 버튼
    public Text Description;  //퀘스트 설명

    [Header("QUEST_LIST")]
    public Dictionary<int, QuestData> QuestList; //데이터 배열을 키 값으로 구분하여 분류 (퀘스트 목록)  
    public List<int> QuestListForSave;//저장 할 데이트를 담는 리스트
    public QuestData[] qData; //데이터를 배열로 가져옴
    public QuestGoal goal;//퀘스트의 달성 조건   

    [Header("QUEST_INDEX")]
    public static int MainQuestIndex = 1000; //현재 메인 퀘스트 인덱스
    public static int SubQuestIndex = 2000; //현재 서브 퀘스트 인덱스

    [Header("HANDLE_QUEST_UI")]
    private Transform[] QuestListText; //퀘스트 리스트를 화면에 보여주기 위한 Text
    private Transform[] InProgressQuestText; //현재 진행중인 퀘스트 리스트를 화면에 보여주기 위한 Text
    private Transform[] ChoiceQuestBtn; //1번 수락 , 2번 거절 ,3번 완료
    private Transform[] temp;

    private GameManager gm;
    private Inventory inventory;


    private List<string> list;

    private void Awake()
    {
         
        QuestListText = new Transform[5];

        temp = QuestListUi.GetComponentsInChildren<Transform>(); //questUi의 텍스트 다 가져오기

        for (int i = 0; i < QuestListText.Length; i++)
        {
            QuestListText[i] = temp[i + 1];
        }
        
       
        InProgressQuestText = new Transform[5];

        temp = InProgressQuestUI.GetComponentsInChildren<Transform>();
        for (int i = 0; i < InProgressQuestText.Length; i++)
        {
            InProgressQuestText[i] = temp[i + 1];
        }

        ChoiceQuestBtn = new Transform[3];
        temp = btn_panel_Choice.GetComponentsInChildren<Transform>();
            ChoiceQuestBtn[0] = temp[1];
            ChoiceQuestBtn[1] = temp[3];
            ChoiceQuestBtn[2] = temp[5];


        Talk.talkData = new Dictionary<int, string[]>();
      
      //  gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        QuestList = new Dictionary<int, QuestData>();

        //메인 퀘스트 1
        SetGoal(QuestGoal.GoalType.FindOther);

        qData[0].setData("큐브와 대화하기"
            , "큐브에게 다가가 스페이스바를 눌러 대화하십시오"
            , "보상 미설정.",new int[] { 100 },true, goal);
        AddQuest(1000, qData[0]); //각각 키에 맞는 퀘스트를 설정해줌

        //서브 퀘스트 1
        SetGoal(QuestGoal.GoalType.GatheringItem, new string[] { "Rock" }, new int[] { 1 });
        qData[1].setData("돌 1개를 모아오세요"
            , "바닥에 떨어져있는 돌멩이를 주워서 가져오세요."
            , "보상 미설정.",false, goal);

        AddQuest(2000, qData[1]); //각각 키에 맞는 퀘스트를 설정해줌

        //서브 퀘스트 2        
        SetGoal(QuestGoal.GoalType.GatheringItem, new string[] { "Rock", "Oil" }, new int[] { 5, 5 });
        qData[2].setData("돌 5개와 오일 5개 모으시오"
            , "돌 5개,오일 5개를 습득하세요"
            , "보상 미설정.",false, goal);

        AddQuest(2001, qData[2]); //각각 키에 맞는 퀘스트를 설정해줌

        //서브 퀘스트 3
        SetGoal(QuestGoal.GoalType.GatheringItem, new string[] { "Rock", "Oil" }, new int[] { 10, 10 });
        qData[3].setData("돌 10개와 오일 10개를 모으시오"
            , "돌 10 개, 오일 10개를 습득하세요"
            , "보상 미설정.",false, goal);

        AddQuest(2002, qData[3]); //각각 키에 맞는 퀘스트를 설정해줌


        //메인 퀘스트 2

        SetGoal(QuestGoal.GoalType.GatheringItem, new string[] { "Rock" }, new int[] { 1 });
        qData[4].setData("바위 파괴하기"
           , "바위에 마우스 왼쪽을 클릭하여 바위를 파괴하시오"
           , "보상은 없습니다.", true, goal);

        AddQuest(1100, qData[4]); //각각 키에 맞는 퀘스트를 설정해줌

        setQuestList();


    }
    private void Start()
    {
        
    }
    private void Update()
    {
        ProgressQuestCheck();
        setQuestList();
    }
    
    public void QuestShowDescription(int index) //퀘스트 리스트 클릭 시 설명창 나타내기 
    {
       
        ChoiceQuestBtn[0].gameObject.SetActive(false);
        ChoiceQuestBtn[1].gameObject.SetActive(false);
        ChoiceQuestBtn[2].gameObject.SetActive(false);
        if (!QuestList[index].isAccept && QuestList[index].isActive)  //퀘스트 수락 가능 상태
        {
            ChoiceQuestBtn[0].gameObject.SetActive(true);
            MessageEffect.setMsg(QuestList[index].description + "을(를) 진행하실 수 있습니다.",true);
        }
        else if (QuestList[index].isAccept && !QuestList[index].goal.IsReached())//퀘스트를 받은 상태
        {
            ChoiceQuestBtn[1].gameObject.SetActive(true); //퀘스트 거절 버튼 
            MessageEffect.setMsg(QuestList[index].questName + "을(를) 진행중입니다.",true);
        }
        else if (QuestList[index].goal.IsReached() && QuestList[index].isAccept) //퀘스트 조건을 만족한 상태
        {
            ChoiceQuestBtn[2].gameObject.SetActive(true);
            MessageEffect.setMsg("오! 부탁한 일을 완료했구만",true);
        }
        
    }
    public void Complete(int QuestIndex)
    {
        QuestList[QuestIndex].Complete();
        ChoiceQuestBtn[2].gameObject.SetActive(false);
        MessageEffect.setMsg(QuestList[QuestIndex].questName+"을(를) 성공하셨습니다.",false); 

    
    }

    public void ShowQuestMenu() { //대화 걸었을 때 퀘스트 창 나타내기
        GameManager.isAction = true;
        QuestMenuUI.SetActive(true);
        btn_panel_Choice.SetActive(false);
        setQuestList();
    }
   public void CloseQuestMenu() //대화 그만 둘 때 퀘스트 창 닫기
    {
        Description.text = "";
        btn_panel_Choice.SetActive(false);
        QuestMenuUI.SetActive(false);
        GameManager.isAction = false;
    }
    public void ShowInProgressQuestUI() //진행중인 퀘스트 유아이 나타내기
    {
        for (int i = 0; i < InProgressQuestText.Length; i++)
        {
            InProgressQuestText[i].gameObject.GetComponent<Text>().text = "";
        }
            SetInProgressQuest();


        if (InProgressQuestUI.activeSelf)
        {
            InProgressQuestUI.SetActive(false);
        }
        else
        {
             InProgressQuestUI.SetActive(true);
        }
    }

    public void CloseInProgressQuest() //진행중인 퀘스트 유아이 닫기
    {
        InProgressQuestUI.SetActive(false);
    }

    public void QuestReceiver(int QuestIndex,bool _Flag) //퀘스트 인덱스 받아서 퀘스트 받기 버튼 클릭시 
    {
        QuestList[QuestIndex].isAccept = _Flag;
        setQuestList();
        btn_panel_Choice.SetActive(false);
        if (_Flag)
        {
            MessageEffect.setMsg(QuestList[QuestIndex].questName + "을(를) 수락하셨습니다..",false);
            
        }
        else
        {
            MessageEffect.setMsg(QuestList[QuestIndex].questName + "을(를) 포기하셨습니다..",false);
        }

    }


    public void SetGoal(QuestGoal.GoalType GoalType, string[] RequireItemName, int[] RequireItemCount) //퀘스트 아이템이 필요한 경우
    {
        goal = new QuestGoal();
        goal.inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        goal.RequireItem = new Dictionary<string, int>();
        goal.goalType = GoalType;
        goal.RequireItemName = RequireItemName;
        for (int i = 0; i < RequireItemName.Length; i++)
        {
            goal.RequireItem.Add(RequireItemName[i],RequireItemCount[i]);
        }
    }

    public void SetGoal(QuestGoal.GoalType GoalType) //아이템이 필요한 퀘스트가 아닐 때
    {
        goal = new QuestGoal();
        goal.inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        goal.RequireItem = new Dictionary<string, int>();
        goal.goalType = GoalType;      
    }
    public void SetInProgressQuest() //현재 진행중인 퀘스트 보여주기
    {
             
        list = new List<string>();
        int count = 0;
        
        if (!CanClearQuest(MainQuestIndex).Equals(""))
        {
            InProgressQuestText[0].gameObject.GetComponent<Text>().text = CanClearQuest(MainQuestIndex);
        }
        else if (!InProgresstQuest(MainQuestIndex).Equals(""))
        {
            InProgressQuestText[0].gameObject.GetComponent<Text>().text = InProgresstQuest(MainQuestIndex);
        }

        for (int i = 0; i < InProgressQuestText.Length; i++)
        {
            
            if (QuestList.ContainsKey(SubQuestIndex + i) && (!CanClearQuest(SubQuestIndex + i).Equals("")))
            {
                list.Add(CanClearQuest(SubQuestIndex + i));
                count++;
            }
            else if (QuestList.ContainsKey(SubQuestIndex + i) && (!InProgresstQuest(SubQuestIndex+i).Equals("")))
                {
                    list.Add(InProgresstQuest(SubQuestIndex + i));
                    count++;
                }         
        }
        for (int k = 0; k < count; k++)
        {
            InProgressQuestText[k+1].gameObject.GetComponent<Text>().text = list[k];
        }
    }

    public void setQuestList() //퀘스트 UI에 퀘스트 배치
    {
        int n = 0;
        int count = 0;
        list = new List<string>();
        for(int i = 0; i<QuestListText.Length-1; i++)
        {
            QuestListText[i+1].gameObject.SetActive(false);
        }

        //메인 퀘스트는 제일 상단에 위치하기
        if (!CanClearQuest(MainQuestIndex).Equals(""))
        {
            QuestListText[0].gameObject.GetComponent<Text>().text = CanClearQuest(MainQuestIndex);
        }
        else if (!CanAcceptQuest(MainQuestIndex).Equals(""))
        {
            QuestListText[0].gameObject.GetComponent<Text>().text = CanAcceptQuest(MainQuestIndex);
        }
        else if (!InProgresstQuest(MainQuestIndex).Equals(""))
        {
            QuestListText[0].gameObject.GetComponent<Text>().text = InProgresstQuest(MainQuestIndex);
        }
        QuestListText[0].gameObject.GetComponent<ClickQuestList>().setId(MainQuestIndex);
        
        //서브 퀘스트는 여러개이므로 반복문으로 배치하기
        for (int i = 0; i < QuestListText.Length; i++)
        {
            if (QuestList.ContainsKey(SubQuestIndex + i))
            {
                if (!CanClearQuest(SubQuestIndex + i).Equals(""))
                {
                    list.Add(CanClearQuest(SubQuestIndex + i));
                    QuestListText[n+1].gameObject.GetComponent<ClickQuestList>().setId(SubQuestIndex + i);
                    count++;
                }
                else if (!CanAcceptQuest(SubQuestIndex + i).Equals(""))
                {
                    list.Add(CanAcceptQuest(SubQuestIndex + i));
                    QuestListText[n+1].gameObject.GetComponent<ClickQuestList>().setId(SubQuestIndex + i);
                    count++;
                }
                else if (!InProgresstQuest(SubQuestIndex + i).Equals(""))
                {
                    list.Add(InProgresstQuest(SubQuestIndex + i));
                    QuestListText[n+1].gameObject.GetComponent<ClickQuestList>().setId(SubQuestIndex + i);
                    count++;
                }else
                {
                    n--;
                }
            }if(n<5)
                n++;
        }
   
        for (int k = 0; k < count; k++)
        {
            QuestListText[k + 1].gameObject.SetActive(true);
            QuestListText[k+1].gameObject.GetComponent<Text>().text = list[k];
        }
    }
    public string InProgresstQuest(int QuestIndex)
    {
        if (QuestList[QuestIndex].isAccept)
            return QuestList[QuestIndex].questName + "          \n진행중...";
        else return "";
    }
    public string CanAcceptQuest(int QuestIndex)
    {
        if (!QuestList[QuestIndex].isAccept && QuestList[QuestIndex].isActive)
            return QuestList[QuestIndex].questName + "          \n퀘스트 진행 가능...";
        else return "";
    }
    public string CanClearQuest(int QuestIndex)
    {
        if (QuestList[QuestIndex].goal.IsReached()&& QuestList[QuestIndex].isAccept)
            return QuestList[QuestIndex].questName + "          \n완료 가능!!";
        else return "";
    }
    public string ClearQuest(int QuestIndex)
    {
        if (!QuestList[QuestIndex].isClear)
            return QuestList[QuestIndex].questName + "          \n퀘스트 완료...";
        else return "";
    }


    public void ProgressQuestCheck()  //실시간으로 퀘스트 진행 상황 체크 ,나중에 시간되면 아이템 획득 or 사용 시에만 적용하면 되서 수정 할 가능성 높음 ,너무 비효율적
    {
            if (QuestList[MainQuestIndex].isAccept && (QuestList[ MainQuestIndex].goal.goalType == QuestGoal.GoalType.GatheringItem))
            {
                switch (QuestList[MainQuestIndex].goal.RequireItemName.Length)
                {
                    case 1:
                        QuestList[MainQuestIndex].goal.CheckQuestItem(QuestList[MainQuestIndex].goal.RequireItemName[0]);
                        break;
                    case 2:
                        QuestList[MainQuestIndex].goal.CheckQuestItem(QuestList[MainQuestIndex].goal.RequireItemName[0], QuestList[MainQuestIndex].goal.RequireItemName[1]);
                        break;
                }
            }
        for (int i = 0; i + SubQuestIndex < SubQuestIndex + 3; i++) //뒤에 3은 서브 퀘스트의 갯수
        {
            if (QuestList.ContainsKey(i + SubQuestIndex))
            {
                if (QuestList[i + SubQuestIndex].isAccept && (QuestList[i + SubQuestIndex].goal.goalType == QuestGoal.GoalType.GatheringItem))
                {
                    switch (QuestList[i + SubQuestIndex].goal.RequireItemName.Length)
                    {
                        case 0:
                            break;
                        case 1:
                            QuestList[i + SubQuestIndex].goal.CheckQuestItem(QuestList[i + SubQuestIndex].goal.RequireItemName[0]);
                            break;
                        case 2:
                            QuestList[i + SubQuestIndex].goal.CheckQuestItem(QuestList[i + SubQuestIndex].goal.RequireItemName[0], QuestList[i + SubQuestIndex].goal.RequireItemName[1]); 
                            break;
                    }
                }
            }
        }
    }
    public void AddQuest(int QuestIndex, QuestData qData)
    {
        QuestList.Add(QuestIndex, qData);
        QuestListForSave.Add(QuestIndex);
    }

    public void LoadToQuest(int _QuestIndex,bool _isActive, bool _isAccept,bool _isClear)
    {
        if (QuestList.ContainsKey(_QuestIndex))
        {
            QuestList[_QuestIndex].setData(_isActive, _isAccept, _isClear);
        }
    }
}
