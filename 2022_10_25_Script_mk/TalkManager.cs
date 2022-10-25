using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{ 
    public Dictionary<int, string[]> talkData; //Ȥ�� ��������� �𸣴ϱ� ������
    public Dictionary<int ,Sprite> portraitData;
    public Sprite[] portraitArr;
    QuestManager quest;

    private void Start()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();

        quest = GameObject.Find("QuestInfo").GetComponent<QuestManager>();
        GenerateData();
    }

    void GenerateData()
    {
        //Ʃ�丮�� ���� ��ȭ
        talkData.Add(0, new string[] { " ����Ű�� �̿��Ͽ� �����̽ÿ�. "}); //����Ʈ �ε����� ��ȭ ���� ����
        talkData.Add(1, new string[] { "�۽�ž�� ���콺 ���� Ŭ���� �Ͽ� ������ �����ʽÿ�." });

        //���� ����Ʈ ���� ��ȭ
        talkData.Add(1000, new string[] { quest.QuestList[1000].description});
        
        //���� ����Ʈ ���� ��ȭ 
        talkData.Add(2000, new string[] { quest.QuestList[2000].description});
        talkData.Add(2001, new string[] { quest.QuestList[2001].description});
        talkData.Add(2002, new string[] { quest.QuestList[2002].description});

        talkData.Add(100, new string[] { "�׳� ����� ť���� �� ����....:0" });
        talkData.Add(1100, new string[] { "ť�긦 �����ߴ�...:0" ,"���� Ư���� ���� ���� �� ����. �۽�ž���� ���ư�����.:0"}); //�ڿ� :0�� �ʻ�ȭ�� �����ϱ� ���� ǥ��
        

        talkData.Add(1300, new string[] { "�� ������ ���Ǿ��̴�.:0" });

        portraitData.Add(100, portraitArr[0]);
        portraitData.Add(1000, portraitArr[0]);
        portraitData.Add(1100, portraitArr[0]); //�ش� ���̵� �ʻ�ȭ�� ���� ���� �� ��ȭ�� �ʻ�ȭ�� ��������� id���� 1�� ���ؼ� �ڿ� ��������Ʈ�� �ٸ� �� ������ ��
       
        portraitData.Add(1300, portraitArr[0]); 


    }
    public string GetTalk(int id, int talkindex)
    {
        if (talkindex == talkData[id].Length)
        {
            if (quest.QuestList[QuestManager.MainQuestIndex].Check_ID[0]+ QuestManager.MainQuestIndex == id && quest.QuestList[QuestManager.MainQuestIndex].isAccept)
            {
                quest.QuestList[QuestManager.MainQuestIndex].goal.Find();
            }
            return null;
        }
        else
            return talkData[id][talkindex];
    }
    public Sprite GetPortrait(int id,int portraitindex)
    {
        return portraitData[id + portraitindex];
    }
}
