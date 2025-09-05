using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapToDisappear : TapToInteractObject
{
    [HideInInspector]
    public bool existing = true;
    public void DisappearAction()
    {
        transform.DOKill(); // Kill any existing tweens
        transform.localScale = Vector3.one; // Reset

        Sequence seq = DOTween.Sequence();

        // Step 1: Bounce up
        seq.Append(transform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 10, 1));

        // Step 2: Shrink to zero after the bounce
        seq.Append(transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack));
    }
    public void AppearAction()
    {
        Sequence seq = DOTween.Sequence()
         .Append(transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack))
         .Append(transform.DOPunchScale(Vector3.one * 0.08f, 0.18f, 8, 1));
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if(!existing) return;
        if (!canInteract) return;
        DisappearAction();
        interactAction.Invoke();
        existing = false;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!existing) return;

        scaleTween?.Kill();
        scaleTween = transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.OutBack);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!existing) return;

        scaleTween?.Kill();
        scaleTween = transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutQuad);
    }

    public void QueueReturnBusItem(Item itemType)
    {
        ReturnBus.Enqueue(itemType, this);
    }
}
