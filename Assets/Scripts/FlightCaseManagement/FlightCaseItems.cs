using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlightCaseItems : MonoBehaviour
{
    public List<ItemInstance> item;
    public Item itemType;
    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;
    public Inventory inventory;

    public void SetComponents()
    {
        if (item != null)
        {
            Color color = ItemSprite.color;
            color.a = 1;
            ItemSprite.color = color;
            ItemSprite.sprite = itemType.Sprite;
            ItemCount.text = item.Count.ToString();
        }
        else
        {
            ItemSprite.sprite = null;
            Color c = ItemSprite.color;
            c.a = 0;
            ItemSprite.color = c;
            ItemCount.text = "";
        }
    }

    public void RemoveItem()
    {
        if (item != null && item.Count > 0)
        {
            inventory.RemoveFromInventory(itemType);
        }
    }
}
