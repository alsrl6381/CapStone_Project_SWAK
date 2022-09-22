using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int,string[]> talkData; //��ųʸ��� Ű�� �������� �����ϴ� ������ ����

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "�ȳ�?" ,"�� ���� ó�� �Ա���?" }); //�տ� ��ȣ�� ������Ʈ�� ���̵� ��Ÿ��

        talkData.Add(100, new string[] { "����� ���� ���ڴ�." });
        talkData.Add(200, new string[] { "������ ������̴�." });
    }

    public string GetTalk(int id,int talkindex) //��ȭ�� ���� ��������
    {
        if (talkindex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkindex];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
