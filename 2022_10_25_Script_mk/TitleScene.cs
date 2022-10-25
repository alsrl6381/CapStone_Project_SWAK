using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class SaveDataForText
{
    [Header("PLAYER_RESOURCE")]
    public Vector3 playerPos;
    
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


public class TitleScene : MonoBehaviour
{
    private SaveNLoad TheSaveLoad;
    private string SaveDataName;
    public GameObject LoadPannel;
    public GameObject Title;
    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    public Text Firsttext;
    public Text Secondtext;
    public Text Thirdtext;


    private SaveDataForText saveForText = new SaveDataForText();

    // Start is called before the first frame update
    void Start()
    {


        //SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";
        SAVE_DATA_DIRECTORY = Application.persistentDataPath + "/Saves/";

        //만약 SAVE_DATA_DIRECTORY가 없다면 만들어 주는 조건
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        }

        setText();
    }
    private void Update()
    {
        CloseLoad();
        setText();
    }

    public void setText()
    {
        LoadText("First", Firsttext);
        LoadText("Second", Secondtext);
        LoadText("Third", Thirdtext);
    }

    public void LoadText(string IndexName, Text text)
    {
        SAVE_FILENAME = "/" + IndexName + "SaveFile.txt";

        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string LoadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveForText = JsonUtility.FromJson<SaveDataForText>(LoadJson);

            text.text =
            "<플레이 시간>" + "생존 일자 :" + saveForText.DayCount.ToString() + "/ 생존 시간:" + saveForText.Time.ToString() + "\n" +
            "<위치>" + "임시 좌표(x,y,z) :" + saveForText.playerPos.x.ToString() + "," + saveForText.playerPos.y.ToString() + "," + saveForText.playerPos.z.ToString() + "\n"+
            "<현재 시나리오>" + saveForText.MainQuestName;
        }
    }
    public void CloseLoad()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadPannel.SetActive(false);
        }
    }

    public void StartScene()
    {
        StartCoroutine("startCoroutine");
    }
    public void ShowLoadList()
    {
        LoadPannel.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadPannel.SetActive(false);
        }
    }
    public void LoadScene(string IndexName)
    {
        SAVE_FILENAME = "/" + IndexName + "SaveFile.txt";
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            StartCoroutine("loadCoroutine", IndexName);
        }
    }
    IEnumerator loadCoroutine(string IndexName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");
            while (!operation.isDone)
        {
            yield return null;
        }
        TheSaveLoad = FindObjectOfType<SaveNLoad>();
        TheSaveLoad.LoadData(IndexName);
        LoadPannel.SetActive(false);
        Title.SetActive(false);
    }
    IEnumerator startCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");
        while (!operation.isDone)
        {
            yield return null;
        }
        LoadPannel.SetActive(false);
        Title.SetActive(false);
    }



}
