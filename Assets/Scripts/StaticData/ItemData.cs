using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[Serializable]
public class ItemData 
{
    public string ItemName;
    public ItemType Type;
    public Sprite Icon;

    [HideIf("Type", ItemType.Equip)]
    public EquipIDs EquipID;
    [HideIf("Type", ItemType.Tools)]
    public ToolIDs ToolID;
    [HideIf("Type", ItemType.Materials)]
    public MaterialIDs MaterialID;
}
