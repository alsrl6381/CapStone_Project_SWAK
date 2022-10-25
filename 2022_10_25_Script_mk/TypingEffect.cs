using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TypingEffect : MonoBehaviour
{
    QuestManager quest;
    private string targetMsg; //대상으로 할 메세지
    public float CharPerSec; //어느 간격으로 나올 지
    private int index;
    Button closebutton;
    Text text;
    bool already;
    bool ShowBtn; //버튼을 보여줘야 하는 텍스트인지 아닌지 판별

    private void Awake()
    {
        text = GetComponent<Text>();
        quest = GameObject.Find("QuestInfo").GetComponent<QuestManager>();
        already = false;
        closebutton = GameObject.Find("Close").GetComponent<Button>();
    }
    public void setMsg(string text,bool ShowBtn) 
    {
        closebutton.gameObject.SetActive(false);
        this.ShowBtn = ShowBtn;
        targetMsg = text;
        EffectStart();
    }
    void EffectStart()
    {
        text.text = "";
        index = 0;

        Invoke("Effecting", 1 / CharPerSec);
        ClickQuestList.LoadingText = true;
    }
    void Effecting()
    {
        quest.btn_panel_Choice.SetActive(false);
        if (targetMsg == text.text)
        {
            EffectEnd();
            return;
        }
        text.text += targetMsg[index];
            index++;
            Invoke("Effecting", 1 / CharPerSec);
       
    }
    void EffectEnd()
    {
        closebutton.gameObject.SetActive(true);
        if (!already)
        {
            quest.btn_panel_Choice.SetActive(ShowBtn);
            already = true;
        }
        else
            already = false;
        ClickQuestList.LoadingText = false;
        quest.btn_panel_Choice.SetActive(ShowBtn);
    }
    
    
}
