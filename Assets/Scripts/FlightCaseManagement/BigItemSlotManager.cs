using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BigItemSlotManager : ItemManagerTemplate
{
    public override void InventoryChanged()
    {
        items = inventory.GetLargeItems();
        if (itemSlots.Count != 0)
        {
            UpdateSlots();
        }
    }
}
