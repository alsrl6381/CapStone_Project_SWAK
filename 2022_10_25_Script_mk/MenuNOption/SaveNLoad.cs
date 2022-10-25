using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class SaveData
{
    [Header("PLAYER_RESOURCE")]
    public Vector3 playerPos;
    public Vector3 playerRot;
    public int HP;
    public int ENERGY;
    

    [Header("ITEM_RESOURCE")]
    public List<int> invenArrayNumber = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();

    [Header("QUEST_RESOURCE")]
    public string MainQuestName;
    public List<int> QuestIndex = new List<int>();
    public List<bool> isActive = new List<bool>();
    public List<bool> isAccept = new List<bool>();
    public List<bool> isClear = new List<bool>();

    [Header("TIME_RESOURCE")]
    public float Time;
    public int DayCount;
    
}

public class SaveNLoad : MonoBehaviour
{

    public SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    [Header("REFERENCE")]
    private Player thePlayer;
    private Inventory theInven;
    private QuestManager theQuest;
    private TimeSystem theTime;
    private Status theStatus;
    private SaveDataForText theTitle;

    // Start is called before the first frame update
    void Start()
    {

        //Application.persistentDataPath 이거를 사용해야 실시간 동기화가 가능함
        //  SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        SAVE_DATA_DIRECTORY = Application.persistentDataPath + "/Saves/";
        //만약 SAVE_DATA_DIRECTORY가 없다면 만들어 주는 조건
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        }


    }


    public void SaveData(string IndexName)
    {
        SAVE_FILENAME = "/"+ IndexName + "SaveFile.txt";

        thePlayer = FindObjectOfType<Player>();
        theInven = FindObjectOfType<Inventory>();
        theQuest = FindObjectOfType<QuestManager>();
        theTime = FindObjectOfType<TimeSystem>();
        theStatus = FindObjectOfType<Status>();

        saveData.playerPos = thePlayer.transform.position;
        saveData.playerRot = thePlayer.transform.eulerAngles;

        


        Slot[] slots = theInven.GetSlots(); //아이템 저장
        for(int i =0; i<slots.Length; i++)
        {
            if(slots[i].item != null)
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemName.Add(slots[i].item.itemName);
                saveData.invenItemNumber.Add(slots[i].itemCount);
            }
        }

        saveData.MainQuestName = theQuest.QuestList[QuestManager.MainQuestIndex].questName;
        for (int i = 0; i < theQuest.QuestList.Count; i++) //퀘스트 저장
        {
            saveData.QuestIndex.Add(theQuest.QuestListForSave[i]);
            saveData.isActive.Add(theQuest.QuestList[theQuest.QuestListForSave[i]].isActive);
            saveData.isAccept.Add(theQuest.QuestList[theQuest.QuestListForSave[i]].isAccept);
            saveData.isClear.Add(theQuest.QuestList[theQuest.QuestListForSave[i]].isClear);
        }

        saveData.Time = theTime.time;
        saveData.DayCount = theTime.DayCount;

        saveData.HP = theStatus.CurrentHp;
        saveData.ENERGY = theStatus.CurrentEnergy;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("저장 완료");
        Debug.Log(json);
    }

    public void LoadData(string IndexName)
    {
        SAVE_FILENAME = "/"+ IndexName + "SaveFile.txt";
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string LoadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(LoadJson);

            

            thePlayer = FindObjectOfType<Player>();
            theInven = FindObjectOfType<Inventory>();
            theQuest = FindObjectOfType<QuestManager>();
            theTime = FindObjectOfType<TimeSystem>();
            theStatus = FindObjectOfType<Status>();

            thePlayer.transform.position = saveData.playerPos;
            thePlayer.transform.eulerAngles = saveData.playerRot;
            

            for(int i =0; i<saveData.invenItemName.Count; i++)
            {
                theInven.LoadToInven(saveData.invenArrayNumber[i], saveData.invenItemName[i], saveData.invenItemNumber[i]);
            }

            for(int i =0; i<saveData.QuestIndex.Count; i++)
            {               
                theQuest.LoadToQuest(saveData.QuestIndex[i], saveData.isActive[i], saveData.isAccept[i], saveData.isClear[i]);
            }

            theTime.time = saveData.Time;
            theTime.DayCount = saveData.DayCount;
            theStatus.CurrentHp = saveData.HP;
            theStatus.CurrentEnergy = saveData.ENERGY;

            Debug.Log("로드 완료");
        }
        else
        {
            Debug.Log("저장 파일이 없습니다.");
        }
    }
    public bool getData(string IndexName)
    {
        SAVE_FILENAME = "/" + IndexName + "SaveFile.txt";
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string LoadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(LoadJson);
            return true;
        }
        else return false;
    }
    }