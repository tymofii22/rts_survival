using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "StaticData/ItemDataList", order = 0)]
public class ItemDataList : ScriptableObject
{
    public List<ItemData> Items;
}
