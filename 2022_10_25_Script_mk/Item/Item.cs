using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public int ItemId;
    public string itemName; // �������� �̸�
    public ItemType itemtype; // �������� ����
    public Sprite itemImage; // �������� �̹���
    public GameObject itemPrefab; // ������ddddd�� ������

    public string weaponType; // ���� ����.

    public enum ItemType
    {
        Ingredient, // ���
        Equipment, // ���
        Used, // ���
        Building, // ���๰
        ETC // ��Ÿ
    }

}
