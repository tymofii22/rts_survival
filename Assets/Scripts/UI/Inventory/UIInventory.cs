using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private ItemDataList dataList;
    [SerializeField] private UISlot slotPrefab;
    [SerializeField] private int slotCount = 30;
    [SerializeField] private InputAction toggleInvAction;

    private List<UISlot> slots = new List<UISlot>();

    private void Start()
    {
        CreateSlots();
        toggleInvAction.Enable();
        toggleInvAction.performed += ToggleInventory;
        gameObject.SetActive(false);
    }

   

    public void SlotSelected(UISlot selectedSlot)
    {
        foreach (UISlot slot in slots)
        {
            if (slot != selectedSlot && slot.IsSelected)
            {
                slot.Deselect();
            }
        }
    }

    private void CreateSlots()
    {
        for (int i = 0; i < slotCount; i++)
        {
            UISlot slot = Instantiate(slotPrefab, transform);
            //slot.SetItemData(itemData);
            slots.Add(slot);
            slot.Init(this);
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
}

