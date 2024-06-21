using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public Button RemoveButton;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        InventoryManager.Instance.ListItems(); // Refresh the inventory list
        Destroy(gameObject); // Destroy the GameObject to ensure it doesn't interfere with other items
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        if (item == null)
        {
            Debug.LogWarning("Item is null. Cannot use item.");
            return; // Exit the method if item is null
        }

        switch (item.itemType)
        {
            case Item.ItemType.Potion:
                Player.Instance.IncreaseHealth(item.value); // Use the potion to increase player's health
                break;
            case Item.ItemType.Scroll:
                Player.Instance.IncreaseExp(item.value);
                break;
        }

        RemoveItem();
    }
}
