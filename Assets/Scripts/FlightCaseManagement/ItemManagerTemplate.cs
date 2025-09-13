using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerTemplate : MonoBehaviour
{
    public enum InventoryUIEvent { AddToStack, NewStack, RemoveFromStack, RemoveStack }

    public List<ItemSlot> itemSlots = new List<ItemSlot>();
    public List<KeyValuePair<Item, List<ItemInstance>>> items = new List<KeyValuePair<Item, List<ItemInstance>>>();
    public Inventory inventory;

    public void OnEnable()
    {
        if (inventory != null)
        {
            inventory.OnItemAddedToExistingStack += OnAddToStack;
            inventory.OnNewItemStackCreated += OnNewStack;
            inventory.OnRemoveItemFromExistingStack += OnRemoveFromStack;
            inventory.OnRemoveItemStack += OnRemoveStack;
        }
    }

    public void OnDisable()
    {
        if (inventory != null)
        {
            inventory.OnItemAddedToExistingStack -= OnAddToStack;
            inventory.OnNewItemStackCreated -= OnNewStack;
            inventory.OnRemoveItemFromExistingStack -= OnRemoveFromStack;
            inventory.OnRemoveItemStack -= OnRemoveStack;
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
        ItemSlotBounce(item, InventoryUIEvent.AddToStack);
    }

    public void OnNewStack(Item item)
    {
        InventoryChanged();
        ItemSlotBounce(item, InventoryUIEvent.NewStack);
    }
    public void OnRemoveFromStack(Item item)
    {
        InventoryChanged();
        ItemSlotBounce(item, InventoryUIEvent.RemoveFromStack);
    }
    public void OnRemoveStack(Item item)
    {
        ItemSlotBounce(item, InventoryUIEvent.RemoveStack);
    }

    ItemSlot FindSlot(Item item)
    {
        foreach (var slot in itemSlots)
            if (slot.itemType == item)
                return slot;
        return null;
    }

    public void ItemSlotBounce(Item item, InventoryUIEvent change)
    {
        var slot = FindSlot(item);
        if (slot == null) return;

        switch (change)
        {
            case InventoryUIEvent.AddToStack: slot.PlayAddToStackTween(); break;
            case InventoryUIEvent.NewStack: slot.PlayNewStackTween(); break;
            case InventoryUIEvent.RemoveFromStack: slot.PlayRemoveFromStackTween(); break; // inward punch
            case InventoryUIEvent.RemoveStack: slot.PlayRemoveStackTween(); break; // enlarge then shrink to 0
        }
    }

    public virtual void InventoryChanged()
    {
    }

    public void GetSlots()
    {
        itemSlots = new List<ItemSlot>(GetComponentsInChildren<ItemSlot>());
    }

    public void UpdateSlots()
    {
        var usedSlots = new HashSet<ItemSlot>();

        // Step 2: place/update items
        foreach (var kvp in items) // each KeyValuePair<Item, List<ItemInstance>>
        {
            // Try to find an existing slot with this item
            var existing = itemSlots.Find(s => s.itemType == kvp.Key);

            if (existing != null)
            {
                // Update existing slot
                existing.item = kvp.Value;
                existing.itemType = kvp.Key;
                existing.SetComponents();
                usedSlots.Add(existing);
            }
            else
            {
                // Find first empty slot
                var empty = itemSlots.Find(s => s.itemType == null);
                if (empty != null)
                {
                    empty.item = kvp.Value;
                    empty.itemType = kvp.Key;
                    empty.SetComponents();
                    usedSlots.Add(empty);
                }
            }
        }

        // Step 3: clear slots not used
        foreach (var slot in itemSlots)
        {
            if (!usedSlots.Contains(slot))
            {
                slot.item = null;
                slot.itemType = null;
                slot.SetComponents();
            }
        }
    }
}
