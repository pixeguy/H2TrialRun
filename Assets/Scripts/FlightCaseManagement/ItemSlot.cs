using DG.Tweening;
using NUnit.Framework;
using System;
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

    public event Action<List<ItemInstance>> ClickedStack;

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

    public virtual void PlayRemoveFromStackTween()
    {
        ItemSprite.transform.DOKill();
        ItemCount.transform.DOKill();

        ItemSprite.transform.localScale = Vector3.one;
        ItemCount.transform.localScale = Vector3.one;

        // inward “deflate” punch
        ItemSprite.transform.DOPunchScale(Vector3.one * -0.12f, 0.22f, 8, 1);
        ItemCount.transform.DOPunchScale(Vector3.one * -0.08f, 0.22f, 8, 1);
    }

    public virtual void PlayRemoveStackTween()
    {
        ItemSprite.transform.DOKill();
        ItemCount.transform.DOKill();

        ItemSprite.transform.localScale = Vector3.one;
        ItemCount.transform.localScale = Vector3.one;

        ItemSprite.transform
            .DOScale(1.15f, 0.10f).SetEase(Ease.OutBack)
            .OnComplete(() => ItemSprite.transform.DOScale(0f, 0.15f).SetEase(Ease.InBack));

        ItemCount.transform
            .DOScale(1.10f, 0.10f).SetEase(Ease.OutBack)
            .OnComplete(() => ItemCount.transform.DOScale(0f, 0.15f).SetEase(Ease.InBack));
        DOVirtual.DelayedCall(0.25f, () =>
        {
            item = null;
            itemType = null;
            SetComponents();
        });
    }

    public void OnClickEvent()
    {
        if (itemType == null) return;
        ClickedStack?.Invoke(item);
    }
}
