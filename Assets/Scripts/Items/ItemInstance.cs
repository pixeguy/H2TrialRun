using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public Item item;
    public ItemInstance(Item item)
    {
        this.item = item;
    }
}
