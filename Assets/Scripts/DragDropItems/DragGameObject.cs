using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragGameObject : MonoBehaviour
{
    protected Vector3 offset;
    protected Camera cam;

    public Action<DragGameObject> onDrag;
    public Action<DragGameObject> onRelease;

    private void Start()
    {
        cam = Camera.main;
    }

    protected virtual void OnClick(Vector3 mousePos)
    {
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(transform.position).z));
        offset = transform.position - mouseWorldPos;
        onDrag?.Invoke(this);
    }

    protected virtual void OnDrag(Vector3 mousePos)
    {
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(transform.position).z));
        transform.position = mouseWorldPos + offset;
    }

    protected virtual void OnRelease(Vector3 mousePos)
    {
        onRelease?.Invoke(this);
    }
}
