using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private ItemDataList dataList;
    [SerializeField] private UISlot slotPrefab;
    [SerializeField] private int invSlotCount = 30;
    [SerializeField] private InputAction toggleInvAction;
    [SerializeField] private Transform slotParent;
    [SerializeField] private Transform craftParent;
    [SerializeField] private int craftSlotSize = 3;
    

    private List<UISlot> slots = new List<UISlot>();
    private List<UISlot> craftSlots = new List<UISlot>();
    private UISlot lastSelected;

    public bool IsInCraft(UISlot slot) =>
       craftSlots.Contains(slot);

    private void Start()
    {
        CreateSlots(invSlotCount);
        CreateCraftSlots(craftSlotSize, craftSlotSize);
        toggleInvAction.Enable();
        toggleInvAction.performed += ToggleInventory;
        gameObject.SetActive(false);
        InventoryDebug();

    }

   

    public void SlotSelected(UISlot selectedSlot)
    {
        if (lastSelected != null && lastSelected.IsSelected)
        {
            if (lastSelected.CurItem != null)
            {
                if ((slots.Contains(lastSelected) && slots.Contains(selectedSlot)) || (craftSlots.Contains(lastSelected) && craftSlots.Contains(selectedSlot)))
                {
                    if (selectedSlot.CurItem != null)
                    {
                        SwapItems(lastSelected, selectedSlot);
                    }
                    else
                    {
                        MoveItem(lastSelected, selectedSlot);

                    }
                }
                else
                {
                    if (selectedSlot.CurItem != null)
                    {
                        
                    }
                    else
                    {
                        MoveBetweenItem(lastSelected, selectedSlot);

                    }
                }
                
                lastSelected.Deselect();
                selectedSlot.Deselect();
                SlotDeselected();

            }
            else
            {
                Debug.Log("Selected Item: Empty Slot");
            }
        }
        else
        {
            lastSelected = selectedSlot;
        }

        foreach (UISlot slot in slots)
        {
            if (slot != selectedSlot && slot.IsSelected)
            {
                slot.Deselect();
            }
        }
    }
    public void SlotDeselected()
    {
        lastSelected = null;
    }

    private void CreateSlots(int count)
    {
        for (int i = 0; i < count; i++)
        {
            UISlot slot = Instantiate(slotPrefab, slotParent);
            //slot.SetItemData(itemData);
            slots.Add(slot);
            slot.Init(this, i%6, i/6);
        }
    }

    private void CreateCraftSlots(int x, int y)
    {
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                UISlot slot = Instantiate(slotPrefab, craftParent);
                //slot.SetItemData(itemData);
                craftSlots.Add(slot);
                slot.Init(this, j, i);
            }
                
        }
    }

     private void ToggleInventory(InputAction.CallbackContext context)
     {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void InventoryDebug()
    {
        slots[0].AddItem(dataList.Items[0]);
        slots[1].AddItem(dataList.Items[1]);
    }

    private void SwapItems(UISlot slot1, UISlot slot2)
    {
        Debug.LogWarning("Swapping Items: " + slot1.CurItem.ItemName + " with " + slot2.CurItem.ItemName);
        if (slot1 == slot2)
        {
            return;
        }
        ItemData tempItem = slot1.CurItem;
        slot1.RemoveItem();
        slot1.AddItem(slot2.CurItem);
        slot2.RemoveItem();
        slot2.AddItem(tempItem);
        }

    private void MoveItem(UISlot fromSlot, UISlot toSlot)
    {
        toSlot.AddItem(fromSlot.CurItem);
        fromSlot.RemoveItem();
    }

    private void MoveBetweenItem(UISlot fromSlot, UISlot toSlot)
    {
        if (IsInCraft(toSlot))
        {
            toSlot.AddItem(fromSlot.CurItem);
            toSlot.Subscribe(fromSlot);
            fromSlot.SwitchLock(false);
        }
        
    }
}

