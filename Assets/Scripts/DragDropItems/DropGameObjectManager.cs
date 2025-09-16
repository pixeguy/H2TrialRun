using System.Collections.Generic;
using UnityEngine;

public class DropGameObjectManager : MonoBehaviour
{
    public List<DropGameObject> items;
    public static DropGameObjectManager instance;
    private void Start()
    {
        instance = this;
        var itemArray = FindObjectsByType<DropGameObject>(FindObjectsSortMode.None);
        foreach (var item in itemArray)
        {
            items.Add(item);
        }
    }

    public void TryAssignItem()
    {
        foreach (var item in items)
        {
            if(item.CheckMousePos() == false) { continue; }
            if (item.CheckItemType(ItemDraggableManager.instance.currentItem.itemType) == false) { continue; }
            else
            {
                ItemDraggableManager.instance.currentItem.transform.position = item.transform.position;
            }
        }
    }
}
