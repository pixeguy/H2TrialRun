using UnityEngine;

public class ItemDraggable : DragDrop
{
    public Item itemType;
    public Inventory inventory;

    public override void OnRelease()
    {
        isDragging = false;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        if(DropGameObjectManager.instance.TryAssignItem(itemType))
        {
            inventory.RemoveFromInventory(itemType);
            Destroy(gameObject);
        } else { Destroy(gameObject); }
        onRelease?.Invoke(this);
    }
}
