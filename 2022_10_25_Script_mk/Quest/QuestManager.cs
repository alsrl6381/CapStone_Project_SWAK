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
    public TypingEffect MessageEffect; //�޽��� õõ�� ������ �ϴ� ���
    public GameObject QuestMenuUI; //����Ʈ ��ü���� UI
    public GameObject QuestListUi; //����Ʈ ��� �����ֱ�
    public GameObject InProgressQuestUI; // �÷��̾ ����Ʈ â ������ �������� ����Ʈ�� ������ UI
    public GameObject btn_panel_Choice; //����Ʈ ���� ���� �Ϸ� ��ư
    public Text Description;  //����Ʈ ����

    [Header("QUEST_LIST")]
    public Dictionary<int, QuestData> QuestList; //������ �迭�� Ű ������ �����Ͽ� �з� (����Ʈ ���)  
    public List<int> QuestListForSave;//���� �� ����Ʈ�� ��� ����Ʈ
    public QuestData[] qData; //�����͸� �迭�� ������
    public QuestGoal goal;//����Ʈ�� �޼� ����   

    [Header("QUEST_INDEX")]
    public static int MainQuestIndex = 1000; //���� ���� ����Ʈ �ε���
    public static int SubQuestIndex = 2000; //���� ���� ����Ʈ �ε���

    [Header("HANDLE_QUEST_UI")]
    private Transform[] QuestListText; //����Ʈ ����Ʈ�� ȭ�鿡 �����ֱ� ���� Text
    private Transform[] InProgressQuestText; //���� �������� ����Ʈ ����Ʈ�� ȭ�鿡 �����ֱ� ���� Text
    private Transform[] ChoiceQuestBtn; //1�� ���� , 2�� ���� ,3�� �Ϸ�
    private Transform[] temp;

    private GameManager gm;
    private Inventory inventory;


    private List<string> list;

    private void Awake()
    {
         
        QuestListText = new Transform[5];

        temp = QuestListUi.GetComponentsInChildren<Transform>(); //questUi�� �ؽ�Ʈ �� ��������

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

        //���� ����Ʈ 1
        SetGoal(QuestGoal.GoalType.FindOther);

        qData[0].setData("ť��� ��ȭ�ϱ�"
            , "ť�꿡�� �ٰ��� �����̽��ٸ� ���� ��ȭ�Ͻʽÿ�"
            , "���� �̼���.",new int[] { 100 },true, goal);
        AddQuest(1000, qData[0]); //���� Ű�� �´� ����Ʈ�� ��������

        //���� ����Ʈ 1
        SetGoal(QuestGoal.GoalType.GatheringItem, new string[] { "Rock" }, new int[] { 1 });
        qData[1].setData("�� 1���� ��ƿ�����"
            , "�ٴڿ� �������ִ� �����̸� �ֿ��� ����������."
            , "���� �̼���.",false, goal);

        AddQuest(2000, qData[1]); //���� Ű�� �´� ����Ʈ�� ��������

        //���� ����Ʈ 2        
        SetGoal(QuestGoal.GoalType.GatheringItem, new string[] { "Rock", "Oil" }, new int[] { 5, 5 });
        qData[2].setData("�� 5���� ���� 5�� �����ÿ�"
            , "�� 5��,���� 5���� �����ϼ���"
            , "���� �̼���.",false, goal);

        AddQuest(2001, qData[2]); //���� Ű�� �´� ����Ʈ�� ��������

        //���� ����Ʈ 3
        SetGoal(QuestGoal.GoalType.GatheringItem, new string[] { "Rock", "Oil" }, new int[] { 10, 10 });
        qData[3].setData("�� 10���� ���� 10���� �����ÿ�"
            , "�� 10 ��, ���� 10���� �����ϼ���"
            , "���� �̼���.",false, goal);

        AddQuest(2002, qData[3]); //���� Ű�� �´� ����Ʈ�� ��������


        //���� ����Ʈ 2

        SetGoal(QuestGoal.GoalType.GatheringItem, new string[] { "Rock" }, new int[] { 1 });
        qData[4].setData("���� �ı��ϱ�"
           , "������ ���콺 ������ Ŭ���Ͽ� ������ �ı��Ͻÿ�"
           , "������ �����ϴ�.", true, goal);

        AddQuest(1100, qData[4]); //���� Ű�� �´� ����Ʈ�� ��������

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
    
    public void QuestShowDescription(int index) //����Ʈ ����Ʈ Ŭ�� �� ����â ��Ÿ���� 
    {
       
        ChoiceQuestBtn[0].gameObject.SetActive(false);
        ChoiceQuestBtn[1].gameObject.SetActive(false);
        ChoiceQuestBtn[2].gameObject.SetActive(false);
        if (!QuestList[index].isAccept && QuestList[index].isActive)  //����Ʈ ���� ���� ����
        {
            ChoiceQuestBtn[0].gameObject.SetActive(true);
            MessageEffect.setMsg(QuestList[index].description + "��(��) �����Ͻ� �� �ֽ��ϴ�.",true);
        }
        else if (QuestList[index].isAccept && !QuestList[index].goal.IsReached())//����Ʈ�� ���� ����
        {
            ChoiceQuestBtn[1].gameObject.SetActive(true); //����Ʈ ���� ��ư 
            MessageEffect.setMsg(QuestList[index].questName + "��(��) �������Դϴ�.",true);
        }
        else if (QuestList[index].goal.IsReached() && QuestList[index].isAccept) //����Ʈ ������ ������ ����
        {
            ChoiceQuestBtn[2].gameObject.SetActive(true);
            MessageEffect.setMsg("��! ��Ź�� ���� �Ϸ��߱���",true);
        }
        
    }
    public void Complete(int QuestIndex)
    {
        QuestList[QuestIndex].Complete();
        ChoiceQuestBtn[2].gameObject.SetActive(false);
        MessageEffect.setMsg(QuestList[QuestIndex].questName+"��(��) �����ϼ̽��ϴ�.",false); 

    
    }

    public void ShowQuestMenu() { //��ȭ �ɾ��� �� ����Ʈ â ��Ÿ����
        GameManager.isAction = true;
        QuestMenuUI.SetActive(true);
        btn_panel_Choice.SetActive(false);
        setQuestList();
    }
   public void CloseQuestMenu() //��ȭ �׸� �� �� ����Ʈ â �ݱ�
    {
        Description.text = "";
        btn_panel_Choice.SetActive(false);
        QuestMenuUI.SetActive(false);
        GameManager.isAction = false;
    }
    public void ShowInProgressQuestUI() //�������� ����Ʈ ������ ��Ÿ����
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

    public void CloseInProgressQuest() //�������� ����Ʈ ������ �ݱ�
    {
        InProgressQuestUI.SetActive(false);
    }

    public void QuestReceiver(int QuestIndex,bool _Flag) //����Ʈ �ε��� �޾Ƽ� ����Ʈ �ޱ� ��ư Ŭ���� 
    {
        QuestList[QuestIndex].isAccept = _Flag;
        setQuestList();
        btn_panel_Choice.SetActive(false);
        if (_Flag)
        {
            MessageEffect.setMsg(QuestList[QuestIndex].questName + "��(��) �����ϼ̽��ϴ�..",false);
            
        }
        else
        {
            MessageEffect.setMsg(QuestList[QuestIndex].questName + "��(��) �����ϼ̽��ϴ�..",false);
        }

    }


    public void SetGoal(QuestGoal.GoalType GoalType, string[] RequireItemName, int[] RequireItemCount) //����Ʈ �������� �ʿ��� ���
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

    public void SetGoal(QuestGoal.GoalType GoalType) //�������� �ʿ��� ����Ʈ�� �ƴ� ��
    {
        goal = new QuestGoal();
        goal.inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        goal.RequireItem = new Dictionary<string, int>();
        goal.goalType = GoalType;      
    }
    public void SetInProgressQuest() //���� �������� ����Ʈ �����ֱ�
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

    public void setQuestList() //����Ʈ UI�� ����Ʈ ��ġ
    {
        int n = 0;
        int count = 0;
        list = new List<string>();
        for(int i = 0; i<QuestListText.Length-1; i++)
        {
            QuestListText[i+1].gameObject.SetActive(false);
        }

        //���� ����Ʈ�� ���� ��ܿ� ��ġ�ϱ�
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
        
        //���� ����Ʈ�� �������̹Ƿ� �ݺ������� ��ġ�ϱ�
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
            return QuestList[QuestIndex].questName + "          \n������...";
        else return "";
    }
    public string CanAcceptQuest(int QuestIndex)
    {
        if (!QuestList[QuestIndex].isAccept && QuestList[QuestIndex].isActive)
            return QuestList[QuestIndex].questName + "          \n����Ʈ ���� ����...";
        else return "";
    }
    public string CanClearQuest(int QuestIndex)
    {
        if (QuestList[QuestIndex].goal.IsReached()&& QuestList[QuestIndex].isAccept)
            return QuestList[QuestIndex].questName + "          \n�Ϸ� ����!!";
        else return "";
    }
    public string ClearQuest(int QuestIndex)
    {
        if (!QuestList[QuestIndex].isClear)
            return QuestList[QuestIndex].questName + "          \n����Ʈ �Ϸ�...";
        else return "";
    }


    public void ProgressQuestCheck()  //�ǽð����� ����Ʈ ���� ��Ȳ üũ ,���߿� �ð��Ǹ� ������ ȹ�� or ��� �ÿ��� �����ϸ� �Ǽ� ���� �� ���ɼ� ���� ,�ʹ� ��ȿ����
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
        for (int i = 0; i + SubQuestIndex < SubQuestIndex + 3; i++) //�ڿ� 3�� ���� ����Ʈ�� ����
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
