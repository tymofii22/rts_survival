using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RecipeData
{
    public ItemID[] TopRow = new ItemID[3];
    public ItemID[] MidRow = new ItemID[3];
    public ItemID[] BottRow = new ItemID[3];
    public ItemID CraftResult;
}
