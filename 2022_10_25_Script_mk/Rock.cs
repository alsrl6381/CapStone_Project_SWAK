using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    [SerializeField]
    private int Hp; //바위의 체력

    [SerializeField]
    private float destroyTime; //파편 삭제 시간

    [SerializeField]
    private Collider col; 

    //필요한 게임 오브젝트
    [SerializeField]
    private GameObject go_rock; //일반 물체
    [SerializeField]
    private GameObject go_debris; //물체 파편

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
