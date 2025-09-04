using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FlightCase : MonoBehaviour
{
    public List<FlightCaseItems> itemSlots = new List<FlightCaseItems>();
    private List<KeyValuePair<Item, List<ItemInstance>>> smallItems = new List<KeyValuePair<Item, List<ItemInstance>>>();
    public Inventory inventory;

    private void Start()
    {
        GetSlots();
    }

    private void Update()
    {
        smallItems = inventory.GetSmallItems();
        if (itemSlots.Count != 0)
        {
            UpdateSlots();
        }
    }

    public void GetSlots()
    {
        itemSlots = new List<FlightCaseItems>(GetComponentsInChildren<FlightCaseItems>());
    }

    public void UpdateSlots()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (i < smallItems.Count)
            {
                var kvp = smallItems[i];
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
