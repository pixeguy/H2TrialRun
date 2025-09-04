using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIButtonFeedback : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    private Tween scaleTween;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        scaleTween?.Kill();
        scaleTween = rectTransform.DOScale(Vector3.one * 1.05f, 0.15f).SetEase(Ease.OutBack);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        scaleTween?.Kill();
        scaleTween = rectTransform.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutQuad);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Optional bounce
        rectTransform.DOKill();
        rectTransform.localScale = Vector3.one;
        rectTransform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 10, 1);
    }
}
