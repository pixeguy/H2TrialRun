using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragGameObject : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;

    public Action<DragGameObject> onDrag;
    public Action<DragGameObject> onRelease;

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnClick()
    {
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(transform.position).z));
        offset = transform.position - mouseWorldPos;
        onDrag.Invoke(this);
    }

    private void OnDrag()
    {
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(transform.position).z));
        transform.position = mouseWorldPos + offset;
    }

    private void OnRelease()
    {
        onRelease.Invoke(this);
    }
}
