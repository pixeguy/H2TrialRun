using Unity.VisualScripting;
using UnityEngine;

public class DragCables : DragGameObject
{
    public Item itemType;
    public MouseObserver mouseObserver;
    private Collider2D col;
    private bool gettingDragged = false;
    public bool assignedSlot = false;
    public Vector3 startPos;

    private void OnEnable()
    {
        startPos = transform.position;
        col = GetComponent<Collider2D>();
        mouseObserver.onMouseClickEvent += OnClick;
        mouseObserver.onMouseDragEvent += OnDrag;
        mouseObserver.onMouseReleaseEvent += OnRelease;
    }
    private void OnDisable()
    {
        mouseObserver.onMouseClickEvent -= OnClick;
        mouseObserver.onMouseDragEvent -= OnDrag;
        mouseObserver.onMouseReleaseEvent -= OnRelease;
    }

    protected override void OnClick(Vector3 mousePos)
    {
        if (!assignedSlot)
        {
            if (col.OverlapPoint(mousePos))
            {
                Vector3 mouseWorldPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(transform.position).z));
                offset = transform.position - mouseWorldPos;
                gettingDragged = true;
                MixerPatchingManager.instance.StartDragging(this);
                onDrag?.Invoke(this);
            }
            else { return; }
        }
    }

    protected virtual void OnDrag(Vector3 mousePos)
    {
        if(!gettingDragged) return;
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(transform.position).z));
        transform.position = mouseWorldPos + offset;
        
    }

    protected override void OnRelease(Vector3 mousePos)
    {
        if (!assignedSlot)
        {
            if (col.OverlapPoint(mousePos))
            {
                gettingDragged = false;
                MixerPatchingManager.instance.StopDragging(mousePos);
                onRelease?.Invoke(this);
            }
            else { transform.position = startPos; }
        }
    }
}
