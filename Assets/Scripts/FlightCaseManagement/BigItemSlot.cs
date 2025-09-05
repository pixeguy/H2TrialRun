using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BigItemSlot : ItemSlot
{
    public override void PlayRemoveFromStackTween()
    {
        ItemSprite.transform.DOKill();  
        ItemCount.transform.DOKill();

        ItemSprite.transform.localScale = Vector3.one;
        ItemCount.transform.localScale = Vector3.one;

        // inward “deflate” punch
        ItemSprite.transform.DOPunchScale(Vector3.one * -0.12f, 0.22f, 8, 1);
        ItemCount.transform.DOPunchScale(Vector3.one * -0.08f, 0.22f, 8, 1);
        DOVirtual.DelayedCall(0.22f, () =>
        {
            ReturnBus.ReturnN(itemType, 1);
        });
    }

    public override void PlayRemoveStackTween()
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
            ReturnBus.ReturnAll(itemType);
            item = null;
            itemType = null;
            SetComponents();
        });
    }

}
