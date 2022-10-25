using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    //ü��
    [SerializeField]
    private int Hp;
    public int CurrentHp;

    //����[������]
    [SerializeField]
    private int Energy;
    public int CurrentEnergy;

    //ä�� ���
    [SerializeField]
    private int MiningPoint;

    //���� ���
    [SerializeField]
    private int FellingPoint;

    //���ᰡ �پ��� �ӵ�
    [SerializeField]
    private int EnergyDecreaseTime;
    private int CurrentEnergyDecreaseTime;

    public int SkillPoints;

    [SerializeField]
    private Image[] Images;

    private const int HP = 0, ENERGY = 1; //HP�� ENERGY�� �ν����� â�� ���� ������� �ֱ�


    // Start is called before the first frame update
    void Start()
    {
        SkillPoints = 0;
        CurrentHp = Hp;
        CurrentEnergy = Energy;
        MiningPoint = 10; //�⺻ ��
        FellingPoint = 10; //�⺻ ��
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
            Debug.Log("���ΰ� �� ���������ϴ�.");
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