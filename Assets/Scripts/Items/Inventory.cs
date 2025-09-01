using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    public List<ItemInstance> itemInventory = new List<ItemInstance>();

    public void AddToInventory(ItemInstance itemInstance)
    {
        itemInventory.Add(itemInstance);
    }
}
