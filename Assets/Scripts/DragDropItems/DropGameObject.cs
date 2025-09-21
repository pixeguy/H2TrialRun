using System;
using UnityEngine;

public class DropGameObject : MonoBehaviour
{
    [SerializeField] Item itemType;
    private Camera cam;
    private Collider2D col;

    public Action<GameObject> objectDropped;

    private void Start()
    {
        cam = Camera.main;
        col = GetComponent<Collider2D>();
    }

    public bool CheckMousePos()
    {
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(transform.position).z));
        Vector2 mouseWorldPos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        if (col.OverlapPoint(mouseWorldPos2D))
        {
            return true;
        }
        return false;
    }

    public bool CheckItemType(Item itemType)
    {
        if (itemType == null) { return false; }
        if(itemType == this.itemType) { return true; }
        return false;
    }

    public bool CheckDrop(Item itemType)
    {
        if (CheckMousePos() == false) { return false; }
        if (CheckItemType(itemType) == false) { return false; }
        else {
            objectDropped?.Invoke(this.gameObject);
            return true; 
        }
    }
}
