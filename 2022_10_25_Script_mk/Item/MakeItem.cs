using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingredient
{
    public Item ingredient; // ��� ������
    public int ingredientCount; // �ʿ��� ����
}


public class MakeItem : MonoBehaviour
{
    // ��Ḧ ���� �迭
    [SerializeField]
    private Ingredient[] ing;
    [SerializeField]
    private Item getitem; // ��ȯ�� ������

    [SerializeField]
    private Inventory inventory;


    public void MakingItem()
    {
        int CanUseItemCount = 0;
        for (int i = 0; i < ing.Length; i++)
        {
            int _itemcount = Checkingredient(ing[i].ingredient.itemName);  // �������� ����
            Debug.Log(_itemcount);
            if (_itemcount >= ing[i].ingredientCount)
            {
                Debug.Log(inventory.ReturnItemCount(ing[i].ingredient.itemName));
                CanUseItemCount++;
            }
            else
            {
                Debug.Log(ing[i].ingredient.itemName + " �������մϴ�.");
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

    // ��� ����ϱ�
    private void UseIngredient()
    {
        Debug.Log("����ó��");
        for (int i = 0; i < ing.Length; i++)
        {
            Debug.Log(ing[i].ingredient.itemName + " �������� " + ing[i].ingredientCount + "�� ���Ǿ����ϴ�.");
            inventory.GiveQuestItem(ing[i].ingredient.itemName, ing[i].ingredientCount);
        }
    }

    // ������ ��ȯ�ϱ�
    private void GetItem()
    {
        Debug.Log(getitem + "��(��) �����Ǿ����ϴ�.");
        inventory.AcquireItem(getitem);
    }

}