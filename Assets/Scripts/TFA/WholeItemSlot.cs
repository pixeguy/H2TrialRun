using DG.Tweening;
using UnityEngine;

public class WholeItemSlot : ItemSlot
{
    public override void PlayRemoveStackTween()
    {
        transform.DOKill();

        transform.localScale = Vector3.one;

        transform
            .DOScale(1.15f, 0.10f).SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                transform.DOScale(0f, 0.15f).SetEase(Ease.InBack);
            });

        DOVirtual.DelayedCall(0.25f, () =>
        {
            Destroy(gameObject);
        });
    }
}
