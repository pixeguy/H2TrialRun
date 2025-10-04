using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour
{
    public virtual bool ReceiveDrop(GameObject obj)
    {
        obj.transform.position = transform.position;
        return true;
    }
}
