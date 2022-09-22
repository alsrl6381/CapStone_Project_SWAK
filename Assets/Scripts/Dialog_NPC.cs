using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable] //자신이 만든 클래스는 public 이여도 시리얼라이져블 선언 해줘야 함
public class Dialog_Text
{
    [TextArea]
    public string []dialog;
}

public class Dialog_NPC : MonoBehaviour
{

    //[SerializeField]는 인스펙터 창에 나타나게 함
    [Header("UI")]
    [SerializeField]private TextMeshProUGUI txt_dailog;
    [SerializeField] private GameObject panel;

    private bool isDalog = false; //대화중인지 아닌지 판단하기
    private int count = 0; //몇 번째 대화인지 확인하기

    public Dialog_Text[] Dialog_Container; //2차원 배열로 나누어서 첫번째 배열은 챕터나 퀘스트 id로 받고 두번째 배열은 텍스트를 나열하면 될 듯
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDalog)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (count < Dialog_Container[0].dialog.Length)
                    NextDailog();
                else
                    Show_Hide(false);
            }
        }
    }

    public void Show_Hide(bool _Flag)
    {
        panel.gameObject.SetActive(_Flag);
        txt_dailog.gameObject.SetActive(_Flag);
        isDalog = _Flag;

        if (_Flag)
        {
            count = 0;
            NextDailog();
        }
    }



    private void NextDailog() //다음 대화로 넘어가기
    {

        txt_dailog.text = Dialog_Container[0].dialog[count];
        count++;
    }


}











