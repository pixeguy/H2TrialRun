using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
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

    public void PlayAddToStackTween()
    {
        // Bounce just the sprite and text
        ItemSprite.transform.DOKill();
        ItemCount.transform.DOKill();

        ItemSprite.transform.localScale = Vector3.one;
        ItemCount.transform.localScale = Vector3.one;

        ItemSprite.transform.DOPunchScale(Vector3.one * 0.15f, 0.25f, 8, 1);
        ItemCount.transform.DOPunchScale(Vector3.one * 0.1f, 0.25f, 8, 1);
    }

    public void PlayNewStackTween()
    {
        // Kill any existing tweens to prevent conflicts
        ItemSprite.transform.DOKill();
        ItemCount.transform.DOKill();

        // Start from scale 0
        ItemSprite.transform.localScale = Vector3.zero;
        ItemCount.transform.localScale = Vector3.zero;

        // Scale up slightly beyond 1, then ease back down
        ItemSprite.transform.DOScale(Vector3.one * 1.2f, 0.2f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                ItemSprite.transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.InOutQuad);
            });

        ItemCount.transform.DOScale(Vector3.one * 1.2f, 0.2f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                ItemCount.transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.InOutQuad);
            });
    }
}
