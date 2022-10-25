using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeSystem : MonoBehaviour
{
    public Text Time_Text;
    public float time;
    public GameObject Needle;
    public float MorningTime;
    public float AfternoonTime;
    public float NightTime;
    public float DawnTime;
    public float EndTime;
    public int DayCount;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Light>().intensity = 1f;
        MorningTime = 0;
        time = MorningTime;
        StartCoroutine(NeedleMove());
        DayCount = 0;
    }

    // Update is called once per frame  0�� - 90 / 9�� - -30 / 
    void Update()
    {
        
        if (!GameManager.isAction)
        {
            time += Time.deltaTime;
        }
        DayAndNight();      
    }
    void DayAndNight() { 
     if (time<AfternoonTime)
        {
            
            Time_Text.text = "��" + ((int) time).ToString()+"/"+DayCount+"����";        
            gameObject.GetComponent<Light>().intensity = 0.8f;
        }
        else if ((time > AfternoonTime) && (time<NightTime))
        {
        
            Time_Text.text = "����" + ((int) time).ToString() + "/" + DayCount + "����"; ;
            gameObject.GetComponent<Light>().intensity = 1f;
        }
        else if ((time > NightTime) && (time<DawnTime))
        {
            
            Time_Text.text = "��" + ((int) time).ToString() + "/" + DayCount + "����"; ;
            gameObject.GetComponent<Light>().intensity = 0.2f;
        }
        else if (time > DawnTime && (time<EndTime))
        {
            Time_Text.text = "����" + ((int)time).ToString() + "/" + DayCount + "����"; ;
            gameObject.GetComponent<Light>().intensity = 0f;
        }
        else if (time >= EndTime)
        {
            time = MorningTime;
            time += Time.deltaTime;
            DayCount++;
        }
       
    }

   IEnumerator NeedleMove() //���� ��ȭ������ ������ ���߸� �ð��� �����Ǵ� ������ ���� �� �ְ� ������ ���� ����̹Ƿ� �ð��� �� �ȸ¾ƶ����� Ȯ�� ����
    {
     
        while (true)
        {
            
            yield return new WaitForSeconds(2f);

            if (!GameManager.isAction)
            {
                Needle.gameObject.GetComponent<RectTransform>().Rotate(0, 0, -360 / NightTime);
            }
            

            if ((Needle.gameObject.GetComponent<RectTransform>().eulerAngles.z == 270))
            {
              
                Needle.gameObject.GetComponent<RectTransform>().Rotate(0, 0, -180);               
            }
        }
        }

    }
    

