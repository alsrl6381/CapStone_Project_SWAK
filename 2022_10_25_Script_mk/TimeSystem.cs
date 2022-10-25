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

    // Update is called once per frame  0시 - 90 / 9시 - -30 / 
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
            
            Time_Text.text = "낮" + ((int) time).ToString()+"/"+DayCount+"일차";        
            gameObject.GetComponent<Light>().intensity = 0.8f;
        }
        else if ((time > AfternoonTime) && (time<NightTime))
        {
        
            Time_Text.text = "오후" + ((int) time).ToString() + "/" + DayCount + "일차"; ;
            gameObject.GetComponent<Light>().intensity = 1f;
        }
        else if ((time > NightTime) && (time<DawnTime))
        {
            
            Time_Text.text = "밤" + ((int) time).ToString() + "/" + DayCount + "일차"; ;
            gameObject.GetComponent<Light>().intensity = 0.2f;
        }
        else if (time > DawnTime && (time<EndTime))
        {
            Time_Text.text = "새벽" + ((int)time).ToString() + "/" + DayCount + "일차"; ;
            gameObject.GetComponent<Light>().intensity = 0f;
        }
        else if (time >= EndTime)
        {
            time = MorningTime;
            time += Time.deltaTime;
            DayCount++;
        }
       
    }

   IEnumerator NeedleMove() //만약 대화등으로 게임이 멈추면 시간이 지연되는 문제가 있을 수 있고 프레임 단위 계산이므로 시간이 딱 안맞아떨어질 확률 많음
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
    

