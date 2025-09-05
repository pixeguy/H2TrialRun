using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SmallItemSlotManager : ItemManagerTemplate
{
    public override void InventoryChanged()
    {
        items = inventory.GetSmallItems();
        if (itemSlots.Count != 0)
        {
            UpdateSlots();
        }
    }
}
