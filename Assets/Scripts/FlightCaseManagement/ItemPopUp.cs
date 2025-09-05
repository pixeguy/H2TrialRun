using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemPopUp : MonoBehaviour
{
    private List<ItemInstance> items;
    private int tempCount;
    public Image itemSprite;
    public TextMeshProUGUI itemCounter;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public Transform removeItemButton;
    public Transform addItemButton;
    public Inventory inventory;
    public UnityEvent onPopUpOpen;
    public UnityEvent onPopUpClose;


    void Start()
    {
        foreach (var slot in FindObjectsByType<ItemSlot>(FindObjectsSortMode.None))
            slot.ClickedStack += SetUpUI;   // subscribe
    }

    void OnDestroy()
    {
        foreach (var slot in FindObjectsByType<ItemSlot>(FindObjectsSortMode.None))
            slot.ClickedStack -= SetUpUI;   // unsubscribe
    }

    private void Update()
    {
        if (items != null)
        {
            itemCounter.text = tempCount.ToString();
            if (tempCount == 0)
            {
                removeItemButton.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                removeItemButton.localScale = new Vector3(1, 1, 1);
            }

            if (tempCount == items.Count)
            {
                addItemButton.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                addItemButton.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void SetUpUI(List<ItemInstance> items)
    {
        transform.localScale = new Vector3(1,1,1);
        this.items = items;
        itemSprite.sprite = items[0].item.Sprite;
        itemName.text = items[0].item.itemName;
        itemDescription.text = items[0].item.itemDescription;
        tempCount = items.Count;

        onPopUpOpen.Invoke();
    }

    public void ChangeTempItemCount(int change)
    {
        tempCount += change;
    }

    public void RemoveFromInventory()
    {
        if (items != null && items.Count > 0)
        {
            inventory.RemoveFromInventory(items[0].item);
        }
    }

    public void ApplyChange()
    {
        int toRemove = items.Count - tempCount;

        // remove that many instances of this item type
        Item type = items[0].item;
        for (int i = 0; i < toRemove; i++)
            inventory.RemoveFromInventory(type);   // your existing API that removes one instance

        // refresh UI
        tempCount = items?.Count ?? 0;             // list is the same reference; count updated
        itemCounter.text = tempCount.ToString();

        transform.localScale = new Vector3(0,0,0);

        onPopUpClose?.Invoke();
    }
}
