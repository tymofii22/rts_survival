using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image select;
    [SerializeField] private Button buttonSelf;
    [SerializeField] private Button buttonDrop;
 
    private ItemData curItem;
    private UIInventory uIInventory;
    private bool isSelected = false;
    private bool isEmpty = true;
    private Vector2Int slotPos;
    private float doubleClickTime;
    private UISlot invSlot;

    public bool IsEmpty => isEmpty;
    public bool IsSelected => isSelected;
    public ItemData CurItem => curItem;
    public UISlot InvSlot => invSlot;

    public void Init(UIInventory uIInventory, int x, int y)
    {
        this.uIInventory = uIInventory;
        slotPos = new Vector2Int(x, y);
        buttonDrop.onClick.AddListener(DropItem);
    }
    public void AddItem(ItemData data)
    {
        itemIcon.sprite = data.Icon;

        Color color = itemIcon.color;
        color.a = 1f;
        itemIcon.color = color;

        curItem = data;
        isEmpty = false;
        buttonDrop.gameObject.SetActive(true);
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
        buttonDrop.gameObject.SetActive(false);
    }

    public void DropItem()
    {
        if (uIInventory.IsInCraft(this))
        {
            RemoveFromCraft();
        }
        else
        {
            uIInventory.DropItem(curItem.id);
            RemoveItem();
        }
    }

    public void SelectItem()
    {
        Debug.LogWarning("SelectItem called on slot at position: " + slotPos);
        if (uIInventory.IsInCraft(this))
        {
            if (Time.time - doubleClickTime < 0.2f && !isEmpty)
            {
                RemoveFromCraft();
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

    private void RemoveFromCraft()
    {
        RemoveItem();
        invSlot.SwitchLock(true);
        Deselect();
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
        invSlot = fromSlot;

    public void SwitchLock(bool state) =>
        buttonSelf.interactable = state;

    private void OnDestroy() =>
        buttonDrop.onClick.RemoveAllListeners();
}
