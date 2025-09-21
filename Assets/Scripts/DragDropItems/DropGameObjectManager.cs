using System;
using System.Collections.Generic;
using UnityEngine;

public class DropGameObjectManager : MonoBehaviour
{
    public List<DropGameObject> items;
    public static DropGameObjectManager instance;
    public SetUpTFA setUp;

    private void OnDestroy()
    {
        setUp.onCreateDropSlot -= AddSlot;
    }

    private void AddSlot(GameObject obj)
    {
        DropGameObject drop = obj.GetComponent<DropGameObject>();
        if (drop != null)
        {
            items.Add(drop);
        }
        else
        {
            Debug.LogWarning("GameObject does not have DropGameObject component attached: " + obj.name);
        }
    }

    private void Start()
    {
        instance = this;
        var itemArray = FindObjectsByType<DropGameObject>(FindObjectsSortMode.None);
        foreach (var item in itemArray)
        {
            items.Add(item);
        }
        setUp.onCreateDropSlot += AddSlot;
    }

    public bool TryAssignItem(Item itemType)
    {
        foreach (var item in items)
        {
            if(item.CheckDrop(itemType)) { return true; }
            else { continue; }
        }
        return false;
    }
}
