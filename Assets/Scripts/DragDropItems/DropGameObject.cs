using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DropGameObject : MonoBehaviour
{
    [SerializeField] public Item itemType;
    public bool specialDrop = false;
    public Item specialItemType;
    private bool isOccupied = false;
    private Camera cam;
    private Collider2D col;

    public Action<GameObject> objectDropped;

    private void Start()
    {
        cam = Camera.main;
        col = GetComponent<Collider2D>();
        if(itemType.itemName == "Analog Mixer")
        {
            specialDrop = true;
            Addressables.LoadAssetsAsync<Item>("AnalogSpeakers", item => { specialItemType = item; });
        }
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
        if(itemType == this.itemType) {
            List<DropGameObject> specialDrop = new();

            foreach (var item in DropGameObjectManager.instance.items)
            {
                if (item.itemType == specialItemType)
                {
                    specialDrop.Add(item);
                }
            }
            foreach(var item in specialDrop)
            {
                if (item.isOccupied == true)
                {
                    isOccupied = true;
                    continue;
                }
                else { return false; }
            }
            return true;
        }
        return false;
    }

    public bool CheckDrop(Item itemType)
    {
        if (CheckMousePos() == false) { return false; }
        if (CheckItemType(itemType) == false) { return false; }
        else {
            objectDropped?.Invoke(this.gameObject);
            isOccupied = true;
            return true; 
        }
    }
}
