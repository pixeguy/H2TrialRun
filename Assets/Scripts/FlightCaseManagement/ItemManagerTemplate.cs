using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerTemplate : MonoBehaviour
{
    public List<ItemSlot> itemSlots = new List<ItemSlot>();
    public List<KeyValuePair<Item, List<ItemInstance>>> items = new List<KeyValuePair<Item, List<ItemInstance>>>();
    public Inventory inventory;

    public void OnEnable()
    {
        if (inventory != null)
        {
            inventory.OnItemAddedToExistingStack += OnAddToStack;
            inventory.OnNewItemStackCreated += OnNewStack;
        }
    }

    public void OnDisable()
    {
        if (inventory != null)
        {
            inventory.OnItemAddedToExistingStack -= OnAddToStack;
            inventory.OnNewItemStackCreated -= OnNewStack;
        }
    }

    private void Start()
    {
        GetSlots();
        InventoryChanged();
    }

    public void OnAddToStack(Item item)
    {
        InventoryChanged();
        ItemSlotBounce(item, false);
    }

    public void OnNewStack(Item item)
    {
        InventoryChanged();
        ItemSlotBounce(item, true);
    }

    public void ItemSlotBounce(Item item, bool isNewStack)
    {
        foreach (var slot in itemSlots)
        {
            if (slot.itemType == item)
            {
                if (isNewStack)
                    slot.PlayNewStackTween(); // start from scale 0
                else
                    slot.PlayAddToStackTween(); // small punch
                break;
            }
        }
    }

    public virtual void InventoryChanged() {
    }

    public void GetSlots()
    {
        itemSlots = new List<ItemSlot>(GetComponentsInChildren<ItemSlot>());
    }

    public void UpdateSlots()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (i < items.Count)
            {
                var kvp = items[i];
                var slot = itemSlots[i];

                // Assign item and type
                slot.item = kvp.Value;
                slot.itemType = kvp.Key;
                slot.SetComponents();
            }
            else
            {
                // Clear unused slots
                var slot = itemSlots[i];
                slot.item = null;
                slot.itemType = null;
                slot.SetComponents();
            }
        }
    }
}
