using UnityEngine;
public enum ItemType
{
    SmallItem,
    LargeItem
}
[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public Sprite Sprite;
    public string itemName;
    public string itemDescription;
    public int maxItemCount;
    public ItemType itemType;
}
