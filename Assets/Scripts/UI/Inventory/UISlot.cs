using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image select;

    private Sprite curItem;
    private UIInventory uIInventory;
    private bool isSelected = false;
    private bool isEmpty = true;

    public bool IsEmpty => isEmpty;
    public bool IsSelected => isSelected;

    public void Init(UIInventory uIInventory)
    {
        this.uIInventory = uIInventory;
    }
    private void AddItem(Sprite itemSprite)
    {
        itemIcon.sprite = itemSprite;

        Color color = itemIcon.color;
        color.a = 1f;
        itemIcon.color = color;

        curItem = itemSprite;
        isEmpty = false;
    }

    private void RemoveItem()
    {
        itemIcon.sprite = null;

        Color color = itemIcon.color;
        color.a = 0f;
        itemIcon.color = color;

        curItem = null;
        isEmpty = true;

        isSelected = false;
    }

    public void SelectItem()
    {
        isSelected = !isSelected;
        
        if (select != null)
        {
            Color color = select.color;
            if (isSelected)
            {
                uIInventory.SlotSelected(this);
                color.a = 0.45f;
            }
            else
            {
                color.a = 0f;
            }
            select.color = color;
        }
    }

    public void Deselect()
    {
        isSelected = false;
        if (select != null)
        {
            Color color = select.color;
            color.a = 0f;
            select.color = color;
        }
    }
}
