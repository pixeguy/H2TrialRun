using UnityEngine;

public class DropCables : DropSlot
{
    public Item itemType;
    public bool CheckItem(Item itemType)
    {
        if (itemType == this.itemType)
        { return true; }
        return false;
    }    

    public bool CheckCable(GameObject obj, Item itemType)
    {
        if(CheckItem(itemType))
        {
            ReceiveDrop(obj);
            return true;
        }
        return false;
    }
}
