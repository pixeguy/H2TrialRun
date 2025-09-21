using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnItemDraggable : MonoBehaviour
{
    public GameObject DraggableItem;
    public TFAWholeItemSlotManager manager;
    public Transform canvas;

    private void Start()
    {
        manager.onCreateSlot += SubscribeNewSlot;
        manager.onDestroySlot += UnSubscribeNewSlot;
    }

    private void OnDestroy()
    {
        manager.onCreateSlot -= SubscribeNewSlot;
        manager.onDestroySlot -= UnSubscribeNewSlot;
    }

    private void SubscribeNewSlot(ItemSlot slot)
    {
        slot.ClickedStack += Spawn;
    }
    private void UnSubscribeNewSlot(ItemSlot slot)
    {
        slot.ClickedStack -= Spawn;
    }

    public void Spawn(List<ItemInstance> items)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
        var obj = Instantiate(DraggableItem, Input.mousePosition, Quaternion.identity,canvas);

        var draggable = obj.GetComponent<ItemDraggable>();
        draggable.rectTransform = draggable.GetComponent<RectTransform>();
        draggable.canvasGroup = draggable.GetComponent<CanvasGroup>();

        var img = obj.GetComponent<Image>();
        img.sprite = items[0].item.Sprite;
        img.SetNativeSize();

        draggable.itemType = items[0].item;
        obj.transform.localScale = Vector3.zero;
        draggable.OnClick();
    }
}