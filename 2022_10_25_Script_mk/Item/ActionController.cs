using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    public GameObject player;
    private Ray ray;
    private RaycastHit hitinfo; // 충돌체 정보 저장

    
    // 화면전체에서 정보를 볼 수 있게
    private float Max_Distance = 20f;
    [SerializeField]
    private LayerMask layermask;

    // 필요한 컴포넌트
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory theInventory;

    private bool pickupActivated = false; // 습득 가능할 시 true


    void Update()
    {
        CheckItem();
        TryAction();
    }


    private void TryAction()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CanPickUP();
        }
    }

    private void CanPickUP()
    {
        if (pickupActivated)
        {
            if(hitinfo.transform != null)
            {
                theInventory.AcquireItem(hitinfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitinfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    private void CheckItem()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitinfo, layermask) && Vector3.Distance(player.transform.position, hitinfo.transform.position) < 3f)
        {
            if (hitinfo.transform.tag == "Item")
            {
                ItemInfoAppear();
                return;
            }

            // 아이템 테그가 아닌 아이템도 추가 예정
        }
        else if (Physics.Raycast(ray, out hitinfo, Max_Distance, layermask))
        {
            if (hitinfo.transform.tag == "Item")
            {
                JustItemInfo();
                return;
            }
        }
        else
        {
            InfoDisappear();
        }

    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitinfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 " + "<color=yellow>" + "[마우스 좌클릭]" + "</color>";
    }

    private void JustItemInfo()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(true);
        actionText.text = hitinfo.transform.GetComponent<ItemPickUp>().item.itemName;
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
}
