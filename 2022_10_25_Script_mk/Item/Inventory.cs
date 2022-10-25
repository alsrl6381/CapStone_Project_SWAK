using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    // 필요한 컴포넌트
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotParent;

    public Slot[] slots;
    public static int CurrentItemCount; //아이템 몇개인지 판별하기 : 김민기

    Dictionary<string, int> itemlist;

    private GameManager gameManager;

    public Slot[] GetSlots() { return slots; }

    [SerializeField]
    private Item[] items; 
    
    public void LoadToInven(int _arrayNum,string _itemName, int _itemNum)
    {
        for(int i=0; i<items.Length; i++)
            if(items[i].itemName == _itemName)
                slots[_arrayNum].AddItem(items[i], _itemNum);
    }

    void Start()
    {
        slots = go_SlotParent.GetComponentsInChildren<Slot>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        TryOpenInventory();
      
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && inventoryActivated)
        {
            inventoryActivated = !inventoryActivated;
            CloseInventory();
        }
    }


    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }


    public void AcquireItem(Item _item, int _count = 1)
    {
        gameManager.ItemLog(_item.name, _count);
        if (Item.ItemType.Equipment != _item.itemtype)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }

            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    CurrentItemCount++; //아이템 몇개인지 판별하기 : 김민기
                    slots[i].AddItem(_item, _count);
                    return;
                }
            }
        }

    }
    public int ReturnItemCount(string _ItemName)
    {
       
        for (int i = 0; i < slots.Length; i++)
        {   
            if (slots[i].item != null) {
                
                if (slots[i].item.itemName == _ItemName)
            {
                    return slots[i].itemCount;
            }
            }
        }
        return 0;
    }
    public void GiveQuestItem(string _ItemName, int _Count) //퀘스트 완료 퀘스트 필요 아이템을 줄이는 함수
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == _ItemName)
                {
                    slots[i].SetSlotCount(-_Count);
                }
            }
        }
    }
}
