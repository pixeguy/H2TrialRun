using System.Linq;
using TMPro;
using UnityEngine;

public class TrackItemCounterInSR : MonoBehaviour
{
    public Item itemType;
    public int itemCount;
    public TextMeshProUGUI textCount;
    public Inventory inventory;

    private void Update()
    {
        textCount.text = itemCount.ToString();
    }

    public void addCount()
    {
        var stack = inventory.itemInventory.FirstOrDefault(pair => pair.Key == itemType);
        var itemcount = 0;
        if (stack.Key != null)
        {
            itemcount = stack.Value.Count;
        }
        if (itemcount + itemCount < itemType.maxItemCount)
        {
            itemCount++;
        }
    }

    public void deductCount()
    {
        if (itemCount > 0)
        {
            itemCount--;
        }
    }
}
