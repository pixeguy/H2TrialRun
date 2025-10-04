using DG.Tweening;
using System;
using UnityEngine;

public class TFAWholeItemSlotManager : ItemManagerTemplate
{
    public enum ItemSize { BigItem, SmallItem};
    public ItemSize itemSize;

    public GameObject itemSlotPrefab;

    public Action<ItemSlot> onCreateSlot;
    public Action<ItemSlot> onDestroySlot;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetSlots();
        }
    }

    public override void GetSlots()
    {
        if (itemSize == ItemSize.BigItem)
        {
            items = inventory.GetLargeItems();
        }
        else { items = inventory.GetSmallItems(); }

        Vector2 startPos = new Vector2(-765f, -384f);

        for (int i = 0; i < items.Count; i++)
        {
            // Spawn the prefab
            GameObject slotGO = Instantiate(itemSlotPrefab, transform);
            // Calculate new position
            Vector2 pos = startPos + new Vector2(i * 150f, 0f);

            // Set anchored position
            RectTransform slotRT = slotGO.GetComponent<RectTransform>();
            slotRT.anchoredPosition = pos;

            // Optionally assign the item info to the slot
            var itemData = items[i];
            var uiSlot = slotGO.GetComponent<WholeItemSlot>();
            if (uiSlot != null)
            {
                itemSlots.Add(uiSlot);
                uiSlot.item = itemData.Value;
                uiSlot.itemType = itemData.Key;
                uiSlot.SetComponents();
                onCreateSlot?.Invoke(uiSlot);
            }

        }
    }

    public override void OnRemoveStack(Item item)
    {

        ItemSlotBounce(item, InventoryUIEvent.RemoveStack);

        var slot = FindSlot(item);
        if (slot == null) return;

        int index = itemSlots.IndexOf(slot);

        // Play the bounce tween first, then chain the shift
        Sequence seq = DOTween.Sequence();

        // Wait until bounce finishes (assuming it's 0.25f seconds)
        float bounceDuration = 3f;

        // Optional: Kill old tweens
        for (int i = index + 1; i < itemSlots.Count; i++)
        {
            if(itemSlots[i] != null)
                itemSlots[i].transform.DOKill();
        }
        DOVirtual.DelayedCall(0.25f, () =>
        {
            for (int i = index + 1; i < itemSlots.Count; i++)
            {
                if(itemSlots[i] == null) continue;
                RectTransform slotTransform = itemSlots[i].GetComponent<RectTransform>();
                Vector3 targetPos = slotTransform.anchoredPosition + new Vector2(-150, 0);
                slotTransform.DOAnchorPos(targetPos, 0.2f).SetEase(Ease.OutQuad);
            }
        });
    }
}
