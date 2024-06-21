using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Add this line to import Text and Image classes

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
        ListItems(); // Update the inventory UI when an item is added
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
        ListItems(); // Update the inventory UI when an item is removed
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in Items) // Change Item to Items
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (EnableRemove.isOn)
                removeButton.gameObject.SetActive(true);
            else
                removeButton.gameObject.SetActive(false);

            obj.GetComponent<InventoryItemController>().AddItem(item); // Ensure each item is properly initialized
        }

        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        foreach (Transform item in ItemContent)
        {
            item.Find("RemoveButton").gameObject.SetActive(EnableRemove.isOn);
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
}
