using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemDraggableManager : MonoBehaviour
{
    public List<ItemDraggable> items;
    public static ItemDraggableManager instance;

    public ItemDraggable currentItem;

    private void OnEnable()
    {
        instance = this;
        var itemArray = FindObjectsByType<ItemDraggable>(FindObjectsSortMode.None);
        foreach (var item in itemArray)
        {
            items.Add(item);
        }
        foreach (var item in items)
        {
            item.onDrag += HandleOnDrag;
            item.onRelease += HandleStopDrag;
        }
    }

    private void OnDisable()
    {
        foreach (var item in items)
        {
            item.onDrag -= HandleOnDrag;
            item.onRelease -= HandleStopDrag;
        }
    }


    private void HandleOnDrag(DragGameObject item)
    {
        if (item is ItemDraggable draggable)
        {
            currentItem = draggable;
        }
    }

    private void HandleStopDrag(DragGameObject _)
    {
        DropGameObjectManager.instance.TryAssignItem();
        currentItem = null;
    }
}
