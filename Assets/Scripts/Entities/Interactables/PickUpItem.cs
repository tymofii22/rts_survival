using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private ItemID itemID;
    private UIInventory inventory;

    public void Init(ItemID itemID, UIInventory inventory)
    {
        this.itemID = itemID;
        this.inventory = inventory;
        Debug.Log("init " + itemID.ToString() + inventory.ToString()); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMove>())
        {
            if (inventory.PickUp(itemID))
            {
                Destroy(gameObject);
            }
        }
    }
}
