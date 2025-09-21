using System;
using Unity.VisualScripting;
using UnityEngine;

public class MouseObserver : MonoBehaviour
{
    private Camera cam;
    public Action<Vector3> onMouseClickEvent;
    public Action<Vector3> onMouseDragEvent;
    public Action<Vector3> onMouseReleaseEvent;
    private Vector3 mousePos;
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        mousePos = GetWorldPos();
    }
    private void OnMouseDown()
    {
        onMouseClickEvent(mousePos);
    }

    private void OnMouseDrag()
    {
        onMouseDragEvent(mousePos);
    }

    private void OnMouseUp()
    {
        onMouseReleaseEvent(mousePos);
    }
    Vector3 GetWorldPos()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        // 3D exact hit
        if (Physics.Raycast(ray, out var hit)) return hit.point;

        // 2D exact hit
        var w = cam.ScreenToWorldPoint(Input.mousePosition);
        var hit2D = Physics2D.Raycast(w, Vector2.zero, 0f);
        if (hit2D.collider) return hit2D.point;

        // Fallback: plane at this object's depth
        float z = cam.WorldToScreenPoint(transform.position).z;
        return cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, z));
    }
}
