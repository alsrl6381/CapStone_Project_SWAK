using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable] //�ڽ��� ���� Ŭ������ public �̿��� �ø���������� ���� ����� ��
public class Dialog_Text
{
    [TextArea]
    public string []dialog;
}

public class Dialog_NPC : MonoBehaviour
{

    //[SerializeField]�� �ν����� â�� ��Ÿ���� ��
    [Header("UI")]
    [SerializeField]private TextMeshProUGUI txt_dailog;
    [SerializeField] private GameObject panel;

    private bool isDalog = false; //��ȭ������ �ƴ��� �Ǵ��ϱ�
    private int count = 0; //�� ��° ��ȭ���� Ȯ���ϱ�

    public Dialog_Text[] Dialog_Container; //2���� �迭�� ����� ù��° �迭�� é�ͳ� ����Ʈ id�� �ް� �ι�° �迭�� �ؽ�Ʈ�� �����ϸ� �� ��
    
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



    private void NextDailog() //���� ��ȭ�� �Ѿ��
    {

        txt_dailog.text = Dialog_Container[0].dialog[count];
        count++;
    }


}











