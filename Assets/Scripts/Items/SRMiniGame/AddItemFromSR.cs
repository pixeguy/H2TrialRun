using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AddItemFromSR : MonoBehaviour
{
    public Inventory inventory;
    private TrackItemCounterInSR[] itemsInSR;

    public void addAllItems(Item FlightCase)
    {
        bool hasFlightCase = inventory.itemInventory.Any(pair => pair.Key == FlightCase);

        if (hasFlightCase)
        {
            ErrorManager.instance.Init("Flight Case is Missing!");
        }
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

    public void ResetAllItems()
    {         itemsInSR = FindObjectsByType<TrackItemCounterInSR>(FindObjectsSortMode.None);
        foreach (var item in itemsInSR)
        {
            item.itemCount = 0;
        }
    }

    public void AddItem(Item itemType)
    {
        var newItem = new ItemInstance(itemType);
        inventory.AddToInventory(newItem);
    }
}
