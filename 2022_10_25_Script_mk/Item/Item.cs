using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public int ItemId;
    public string itemName; // 아이템의 이름
    public ItemType itemtype; // 아이템의 유형
    public Sprite itemImage; // 아이템의 이미지
    public GameObject itemPrefab; // 아이템ddddd의 프리팹

    public string weaponType; // 무기 유형.

    public enum ItemType
    {
        Ingredient, // 재료
        Equipment, // 장비
        Used, // 사용
        Building, // 건축물
        ETC // 기타
    }

}
