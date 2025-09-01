using System.Collections.Generic;
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
}
