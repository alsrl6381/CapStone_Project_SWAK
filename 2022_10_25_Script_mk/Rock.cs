using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    [SerializeField]
    private int Hp; //������ ü��

    [SerializeField]
    private float destroyTime; //���� ���� �ð�

    [SerializeField]
    private Collider col; 

    //�ʿ��� ���� ������Ʈ
    [SerializeField]
    private GameObject go_rock; //�Ϲ� ��ü
    [SerializeField]
    private GameObject go_debris; //��ü ����

   public void Mining(int Damage)
    {
        Hp-=Damage;
        if (Hp <= 0)
        {
            Destruction();
        }
    }
    private void Destruction()
    {
        col.enabled = false;
        Destroy(go_rock);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
