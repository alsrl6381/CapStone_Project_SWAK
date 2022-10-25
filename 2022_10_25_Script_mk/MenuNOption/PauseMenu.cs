using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject savePannel;
    public SaveNLoad save;
    public Text[] text;

    public TitleScene title;
    
    public void ClickContinue()
    {
        gameObject.SetActive(false);
    }
    public void ClickSaveList(string IndexName)
    {
        save.SaveData(IndexName);
        SetDataText(IndexName);
    }
    public void ClickSave()
    {
        savePannel.SetActive(true);

        ShowSaveData("First");
        ShowSaveData("Second");
        ShowSaveData("Third");
    }

    public void ClickOption()
    {

    }
    public void ClickExit()
    {
        Title.instance.gameObject.SetActive(true);
        SceneManager.LoadScene("TitleScene");      
    }
    public void SetDataText(string seq)
    {
        int listIndex = 0;
        if (seq.Equals("First"))
        {
            listIndex = 0;
        }else if (seq.Equals("Second"))
        {
            listIndex = 1;
        }
        else if(seq.Equals("Third"))
        {
            listIndex = 2;
        }
        text[listIndex].text =
            "<플레이 시간>" + "생존 일자 :" + save.saveData.DayCount.ToString() + "/ 생존 시간:" + save.saveData.Time.ToString() + "\n" +
            "<위치>" + "임시 좌표(x,y,z) :" + save.saveData.playerPos.x.ToString() + "," + save.saveData.playerPos.y.ToString() + "," + save.saveData.playerPos.z.ToString() + "\n" +
            "<현재 시나리오>" + save.saveData.MainQuestName;
    }
    public void ShowSaveData(string IndexName)
    {
        if (save.getData(IndexName))
        {
            SetDataText(IndexName);
        }
        
    } 
    }

