using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public TextMeshProUGUI QuestText;
    public GameObject panel;

    public QuestData []qData; //�����͸� �迭�� ������

    public QuestGoal goal;
 
    Dictionary<int, QuestData> QuestList; //������ �迭�� Ű ������ �����Ͽ� �з� (����Ʈ ���)  


    private void Start()
    {
        //����Ʈ 1
        QuestList = new Dictionary<int, QuestData>();

        goal.goalType = QuestGoal.GoalType.FindOther; //����Ʈ�� ����
        goal.requiredAmount = 1;

        qData[0].setData(0, "�糪�� ��ȭ�ϱ�"
            ,"�糪���� �ٰ��� ���콺 ���� Ŭ���� �Ͽ� ��ȭ�Ͻʽÿ�"
            , "������ �����ϴ�.", goal);
            
        QuestList.Add(0, qData[0]); //���� Ű�� �´� ����Ʈ�� ��������
                                    
        //����Ʈ 2
        goal.goalType = QuestGoal.GoalType.Gathering;
        goal.requiredAmount = 10;

        qData[1].setData(1, "������ 10�� �����ÿ�"
            , "������ ã�� ä�� �ϼ���"
            , "������ �����ϴ�.", goal);

        QuestList.Add(1, qData[1]); //���� Ű�� �´� ����Ʈ�� ��������
    }


    public void OpenQuestWindow()
    {
        panel.SetActive(true);
       // QuestText.text = qData.description;
    }
    public void CloseQuestWindow()
    {
        panel.SetActive(false);
    }
}
