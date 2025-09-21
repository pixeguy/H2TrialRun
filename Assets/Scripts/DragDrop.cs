using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler
{
    [HideInInspector]
    public RectTransform rectTransform;
    public CanvasGroup canvasGroup;

    public Action<DragDrop> onDrag;
    public Action<DragDrop> onRelease;

    private Vector3 offset;
    protected bool isDragging = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnClick();
    }

    public virtual void OnClick()
    {
        isDragging = true;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
        offset = (Vector3)rectTransform.anchoredPosition - Input.mousePosition;
        onDrag?.Invoke(this);
    }

    public virtual void OnDragging()
    {
        isDragging = true;
        rectTransform.anchoredPosition = Input.mousePosition + offset;
    }

    public virtual void OnRelease()
    {
        isDragging = false;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        onRelease?.Invoke(this);
    }

    private void Update()
    {
        if(isDragging)
        {
            OnDragging();

            if (Input.GetMouseButtonUp(0))
            {
                OnRelease();
            }
        }
    }
}
