using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class UICraft : MonoBehaviour
{
    [SerializeField] private RecipeDataList recipes;
    [SerializeField] private UIInventory inventory;
    [SerializeField] private ItemDataList dataList;

    private ItemID[,] itemIDs = new ItemID[3, 3];

    public void AddItem(ItemID iD, Vector2Int coords)
    {
        itemIDs[coords.x, coords.y] = iD;
        CheckRecipe();
    }

    public void RemoveItem(Vector2Int coords)
    {
        itemIDs[coords.x, coords.y] = ItemID.None;
        CheckRecipe();
    }

    private void CheckRecipe()
    {
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

    }
}