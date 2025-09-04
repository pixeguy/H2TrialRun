using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogBook : MonoBehaviour
{
    public TextMeshPro list;
    public Inventory inventory;
    private List<KeyValuePair<Item, List<ItemInstance>>> itemList = new();
    public void GetInventory()
    {
        itemList = inventory.itemInventory;
    }

    public void Update()
    {
        string output = "";

        foreach (var pair in itemList)
        {
            Item item = pair.Key;
            List<ItemInstance> instances = pair.Value;

            output += $"{item.name} x{instances.Count}\n";
        }

        list.text = output;
    }
}
