using UnityEngine;

public class ForceInventoryChange : MonoBehaviour
{
    public Inventory PlayerInventory;

    public void ChangeToScene2Inventory(Inventory inv)
    {
        PlayerInventory.CopyFrom(inv);
    }
}
