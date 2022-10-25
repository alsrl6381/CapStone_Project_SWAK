using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ClickQuestList : MonoBehaviour
{
    public int QuestID=0; //����Ʈ ���̵� ��ƿ��� ����
    public Button btn;
    public QuestManager questManager;
    public static int ChoiceButtonId; //����ƽ���� �����Ͽ� ����,����,�Ϸ� ��ư �����ÿ� ���
    public static bool LoadingText;

    private void Start()
    {
        btn = gameObject.GetComponent<Button>();
        questManager = GameObject.Find("QuestInfo").GetComponent<QuestManager>();
    }

    public void setId(int QuestID) //�ش� ��ư�̳� ����Ʈ�� ����Ʈ�� ���̵� ��������
    {
        this.QuestID = QuestID;
    }
    public void ClickItem() //����Ʈ ����Ʈ�� ������ �� ����Ʈ ���̵� �ش��ϴ� ������ ������
    {
        if (QuestID != 0 && !LoadingText)
        {            
            questManager.QuestShowDescription(QuestID);
            ChoiceButtonId = QuestID;
        }
    }
    public void AcceptClick() //������ư ������ �� �۵�
    {
        questManager.QuestReceiver(ChoiceButtonId,true);
    }
    public void Complete() //����Ʈ �Ϸ� ��ư ���� �� �۵�
    {
        questManager.Complete(ChoiceButtonId);
        questManager.setQuestList();
    }
    public void RefuseClick() {
        questManager.QuestReceiver(ChoiceButtonId,false);


    }
}
