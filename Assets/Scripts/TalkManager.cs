using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int,string[]> talkData; //딕셔너리는 키와 벨류값을 저장하는 데이터 구조

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?" ,"이 곳에 처음 왔구나?" }); //앞에 번호는 오브젝트의 아이디를 나타냄

        talkData.Add(100, new string[] { "평범한 나무 상자다." });
        talkData.Add(200, new string[] { "길쭉한 원기둥이다." });
    }

    public string GetTalk(int id,int talkindex) //대화한 내용 내보내기
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
