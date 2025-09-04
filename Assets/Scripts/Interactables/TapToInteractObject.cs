using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TapToInteractObject : InteractibleObject, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public UnityEvent interactAction;
    private Tween scaleTween;

    public void OnPointerDown(PointerEventData eventData)
    {
        scaleTween?.Kill();
        scaleTween = transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        scaleTween?.Kill();
        scaleTween = transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutQuad);
    }

    public override void Interact()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canInteract) return;
        BounceAction();
        interactAction.Invoke();
    }
}
