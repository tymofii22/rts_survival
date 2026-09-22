using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemFactory : MonoBehaviour
{
    [SerializeField] private UIInventory inventory;
    [SerializeField] private ItemDataList dataList;
    [SerializeField] private List<SpawnableID> startSpawnables = new List<SpawnableID>();

    private void Start()
    {
        for (int i = 0; i < startSpawnables.Count; i++)
        {
            CreateItem(startSpawnables[i].Id, startSpawnables[i].transform.position);
        }
    }

    private void CreateItem(ItemID id, Vector3 pos)
    {
        PickUpItem prefab = Resources.Load<PickUpItem>("Prefabs/Items/" + id.ToString());
        PickUpItem item = Instantiate(prefab, pos, Quaternion.identity);
    }
}
