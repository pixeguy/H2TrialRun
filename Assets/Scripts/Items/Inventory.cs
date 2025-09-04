using System.Collections.Generic;
using System.Linq;
using UnityEngine;



[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    public List<KeyValuePair<Item, List<ItemInstance>>> itemInventory = new();

    private void OnEnable()
    {
        itemInventory.Clear();
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
        }
        else
        {
            // create a new pair with a new list
            itemInventory.Add(new KeyValuePair<Item, List<ItemInstance>>(itemInstance.item, new List<ItemInstance> { itemInstance }));
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
                    itemInventory.RemoveAt(i);

                break;
            }
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
