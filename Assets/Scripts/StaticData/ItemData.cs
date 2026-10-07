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
    public ItemID id;
    
}

public enum ItemID
{
    None = 0,
    Backpack = 1,
    Rope = 2,
    Wood = 3,
    Stone = 4,
    Tape = 5
}