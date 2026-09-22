using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image select;
    [SerializeField] private Button button;
 
    private ItemData curItem;
    private UIInventory uIInventory;
    private bool isSelected = false;
    private bool isEmpty = true;
    private Vector2Int slotPos;
    private float doubleClickTime;
    private UISlot fromSlot;

    public bool IsEmpty => isEmpty;
    public bool IsSelected => isSelected;
    public ItemData CurItem => curItem;

    public void Init(UIInventory uIInventory, int x, int y)
    {
        this.uIInventory = uIInventory;
        slotPos = new Vector2Int(x, y);
    }
    public void AddItem(ItemData data)
    {
        itemIcon.sprite = data.Icon;

        Color color = itemIcon.color;
        color.a = 1f;
        itemIcon.color = color;

        curItem = data;
        isEmpty = false;
    }

    public void RemoveItem()
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
        Debug.LogWarning("SelectItem called on slot at position: " + slotPos);
        if (uIInventory.IsInCraft(this))
        {
            if (Time.time - doubleClickTime < 0.2f && !isEmpty)
            {
                RemoveItem();
                fromSlot.SwitchLock(true);
                Deselect();
                return;
            }
            doubleClickTime = Time.time;
        }
        isSelected = !isSelected;
        
        if (select != null)
        {
            Color color = select.color;
            if (isSelected)
            {
                color.a = 0.45f;
                select.color = color;
                uIInventory.SlotSelected(this);
            }
            else
            {
                color.a = 0f;
                select.color = color;
                uIInventory.SlotDeselected();
            }
        }
    }

    public void Deselect()
    {
        Debug.LogWarning("Deselect called on slot at position: " + slotPos);
        isSelected = false;
        if (select != null)
        {
            Color color = select.color;
            color.a = 0f;
            select.color = color;
        }
    }

    public void Subscribe(UISlot fromSlot) =>
        this.fromSlot = fromSlot;

    public void SwitchLock(bool state) =>
        button.interactable = state;
}
