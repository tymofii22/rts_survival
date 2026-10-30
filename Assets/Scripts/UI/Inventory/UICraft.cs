using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class UICraft : MonoBehaviour
{
    [SerializeField] private RecipeDataList recipes;
    [SerializeField] private UIInventory inventory;
    [SerializeField] private ItemDataList dataList;

    private ItemID[,] itemIDs = new ItemID[3, 3];
    private ItemID curItem;
    private Dictionary<UISlot, UISlot> cleanSlots = new Dictionary<UISlot, UISlot>();

    public void AddItem(ItemID iD, Vector2Int coords, UISlot craftSlot, UISlot invSlot)
    {
        itemIDs[coords.x, coords.y] = iD;
        CheckRecipe();
        cleanSlots.Add(craftSlot, invSlot);
        Debug.Log("AddItem " + invSlot);
    }

    public void RemoveItem(Vector2Int coords, UISlot craftSlot)
    {
        itemIDs[coords.x, coords.y] = ItemID.None;
        CheckRecipe();
        cleanSlots.Remove(craftSlot);
    }

    private void CheckRecipe()
    {
        curItem = ItemID.None;
        for (int i = 0; i < recipes.Recipes.Count; i++)
        {
            if (CheckRow(recipes.Recipes[i].TopRow, 0) && 
                CheckRow(recipes.Recipes[i].MidRow, 1) && 
                CheckRow(recipes.Recipes[i].BottRow, 2))
            {
                //show recipe icon
                foreach (ItemData item in dataList.Items)
                {
                    if (item.id == recipes.Recipes[i].CraftResult)
                    {
                        inventory.ChangePreview(item.Icon);
                        curItem = item.id;
                    }
                }
            }
            else
            {
                inventory.ChangePreview(null);
            }
        }
        bool CheckRow(ItemID[] row, int n)
        {
            for (int i = 0; i < itemIDs.GetLength(0); i++)
            {
                if (row[i] != itemIDs[i, n])
                {
                    return false;
                }
            }
            return true;
        }
    }

    public void CraftItem()
    {
        if (curItem != ItemID.None)
        {
            ItemID id = curItem;
            Debug.Log("craft " + cleanSlots.Keys.Count);
            for (int i = 0; i < cleanSlots.Keys.Count;)
            {
                Debug.Log("craft " + cleanSlots.ElementAt(i).Value);
                cleanSlots.ElementAt(i).Value.RemoveItem();
                cleanSlots.ElementAt(i).Key.RemoveFromCraft();
                //cleanSlots.Remove(cleanSlots.ElementAt(0).Key);
            }
            inventory.PickUp(id);
        }
    }
}