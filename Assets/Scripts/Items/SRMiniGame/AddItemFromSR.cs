using System.Collections.Generic;
using UnityEngine;

public class AddItemFromSR : MonoBehaviour
{
    public Inventory inventory;
    private TrackItemCounterInSR[] itemsInSR;

    public void addAllItems()
    {
        itemsInSR = FindObjectsByType<TrackItemCounterInSR>(FindObjectsSortMode.None);
        foreach(var item in itemsInSR)
        {
            for(int i = 0; i < item.itemCount; i++)
            {
                var newItem = new ItemInstance(item.itemType);
                inventory.AddToInventory(newItem);
            }
            item.itemCount = 0;
        }
    }

    public void AddItem(Item itemType)
    {
        var newItem = new ItemInstance(itemType);
        inventory.AddToInventory(newItem);
    }
}
