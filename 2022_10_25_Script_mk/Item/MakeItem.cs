using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingredient
{
    public Item ingredient; // 재료 아이템
    public int ingredientCount; // 필요한 개수
}


public class MakeItem : MonoBehaviour
{
    // 재료를 넣을 배열
    [SerializeField]
    private Ingredient[] ing;
    [SerializeField]
    private Item getitem; // 반환할 아이템

    [SerializeField]
    private Inventory inventory;


    public void MakingItem()
    {
        int CanUseItemCount = 0;
        for (int i = 0; i < ing.Length; i++)
        {
            int _itemcount = Checkingredient(ing[i].ingredient.itemName);  // 재료아이템 개수
            Debug.Log(_itemcount);
            if (_itemcount >= ing[i].ingredientCount)
            {
                Debug.Log(inventory.ReturnItemCount(ing[i].ingredient.itemName));
                CanUseItemCount++;
            }
            else
            {
                Debug.Log(ing[i].ingredient.itemName + " 가부족합니다.");
            }
        }
        Debug.Log(CanUseItemCount);
        if (CanUseItemCount == ing.Length)
        {
            UseIngredient();
            GetItem();
        }
    }

    private int Checkingredient(string _name)
    {
        int _ingredientCount = inventory.ReturnItemCount(_name);
        return _ingredientCount;
    }

    // 재료 사용하기
    private void UseIngredient()
    {
        Debug.Log("삭제처리");
        for (int i = 0; i < ing.Length; i++)
        {
            Debug.Log(ing[i].ingredient.itemName + " 아이템이 " + ing[i].ingredientCount + "개 사용되었습니다.");
            inventory.GiveQuestItem(ing[i].ingredient.itemName, ing[i].ingredientCount);
        }
    }

    // 아이템 반환하기
    private void GetItem()
    {
        Debug.Log(getitem + "이(가) 생성되었습니다.");
        inventory.AcquireItem(getitem);
    }

}