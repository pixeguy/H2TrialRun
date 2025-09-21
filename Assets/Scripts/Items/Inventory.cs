using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ItemStack
{
    public Item item;
    public List<ItemInstance> instances = new();
}

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    public List<ItemStack> presetStacks = new(); // for inspector editing

    public List<KeyValuePair<Item, List<ItemInstance>>> itemInventory = new();

    public event Action<Item> OnItemAddedToExistingStack;
    public event Action<Item> OnNewItemStackCreated;
    public event Action<Item> OnRemoveItemFromExistingStack;
    public event Action<Item> OnRemoveItemStack;

    private void OnEnable()
    {
        itemInventory.Clear();
        foreach (var stack in presetStacks)
        {
            if (stack.item != null && stack.instances != null && stack.instances.Count > 0)
            {
                // You can deep-copy if needed:
                itemInventory.Add(new KeyValuePair<Item, List<ItemInstance>>(
                    stack.item,
                    new List<ItemInstance>(stack.instances)
                ));
            }
        }
    }

    public void AddToInventory(ItemInstance itemInstance)
    {
        int idx = itemInventory.FindIndex(kv => kv.Key == itemInstance.item);
        if (idx >= 0)
        {
            // mutate the existing list (safe)
            var stack = itemInventory[idx].Value;
            if (stack == null)
            {
                stack = new List<ItemInstance>();
                itemInventory[idx] = new KeyValuePair<Item, List<ItemInstance>>(itemInventory[idx].Key, stack);
            }
            stack.Add(itemInstance);
            OnItemAddedToExistingStack?.Invoke(itemInstance.item);
        }
        else
        {
            // create a new pair with a new list
            itemInventory.Add(new KeyValuePair<Item, List<ItemInstance>>(itemInstance.item, new List<ItemInstance> { itemInstance }));
            OnNewItemStackCreated?.Invoke(itemInstance.item);
        }
    }

    public void RemoveFromInventory(Item itemToRemove)
    {
        for (int i = 0; i < itemInventory.Count; i++)
        {
            if (itemInventory[i].Key == itemToRemove)
            {
                var instanceList = itemInventory[i].Value;

                if (instanceList.Count > 0)
                    instanceList.RemoveAt(0);

                // Optional: remove the whole entry if list is now empty
                if (instanceList.Count == 0)
                {
                    itemInventory.RemoveAt(i);
                    OnRemoveItemStack?.Invoke(itemToRemove);
                }
                else
                {
                    OnRemoveItemFromExistingStack?.Invoke(itemToRemove);
                }
                break;
            }
        }
    }

    public void CopyFrom(Inventory other)
    {
        // Clear current inventory
        itemInventory.Clear();

        // Copy items from other inventory
        foreach (var kvp in other.itemInventory)
        {
            // Clone the item instances
            List<ItemInstance> clonedList = new List<ItemInstance>();
            foreach (var instance in kvp.Value)
            {
                // Assuming you want a shallow copy of instances (or make a deep copy if needed)
                ItemInstance newInstance = new ItemInstance(instance.item);
                clonedList.Add(newInstance);
            }

            itemInventory.Add(new KeyValuePair<Item, List<ItemInstance>>(kvp.Key, clonedList));

            // Fire event to notify something was added
            if (clonedList.Count > 1)
                OnItemAddedToExistingStack?.Invoke(kvp.Key);
            else
                OnNewItemStackCreated?.Invoke(kvp.Key);
        }
    }

    public List<KeyValuePair<Item, List<ItemInstance>>> GetSmallItems()
    {
        return itemInventory
            .Where(kvp => kvp.Key.itemType == ItemType.SmallItem)
            .ToList();
    }

    public List<KeyValuePair<Item, List<ItemInstance>>> GetLargeItems()
    {
        return itemInventory
            .Where(kvp => kvp.Key.itemType == ItemType.LargeItem)
            .ToList();
    }
}
