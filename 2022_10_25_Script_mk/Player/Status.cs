using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    //체력
    [SerializeField]
    private int Hp;
    public int CurrentHp;

    //연료[에너지]
    [SerializeField]
    private int Energy;
    public int CurrentEnergy;

    //채광 기술
    [SerializeField]
    private int MiningPoint;

    //벌목 기술
    [SerializeField]
    private int FellingPoint;

    //연료가 줄어드는 속도
    [SerializeField]
    private int EnergyDecreaseTime;
    private int CurrentEnergyDecreaseTime;

    public int SkillPoints;

    [SerializeField]
    private Image[] Images;

    private const int HP = 0, ENERGY = 1; //HP와 ENERGY를 인스펙터 창에 들어온 순서대로 넣기


    // Start is called before the first frame update
    void Start()
    {
        SkillPoints = 0;
        CurrentHp = Hp;
        CurrentEnergy = Energy;
        MiningPoint = 10; //기본 값
        FellingPoint = 10; //기본 값
    }

    // Update is called once per frame 
    void Update()
    {
        GaugeUpdate();
        UseEnergy();
    }
    private void UseEnergy()
    {
        if (CurrentEnergy > 0)
        {
            if (CurrentEnergyDecreaseTime <= EnergyDecreaseTime)
            {
                CurrentEnergyDecreaseTime++;
            }
            else
            {
                CurrentEnergy--;
                CurrentEnergyDecreaseTime = 0;
            }
        }
        else
        {
            Debug.Log("연로가 다 떨어졌습니다.");
        }
    }
    private void GaugeUpdate()
    {
        Images[HP].fillAmount = (float)CurrentHp / Hp;
        Images[ENERGY].fillAmount = (float)CurrentEnergy / Energy;
    }

    private void UpMiningPoint()
    {
        MiningPoint += 100;
    }
    private void UpFellingPoint()
    {
        FellingPoint += 100;
    }
}