using Unity.VisualScripting;
using UnityEngine;

public class MixerPatchingManager : MonoBehaviour
{
    public static MixerPatchingManager instance;
    public DragCables currentDragging;

    private void Awake()
    {
        instance = this;
    }

    public void StartDragging(DragCables obj) => currentDragging = obj;
    public void StopDragging(Vector3 position)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(worldPos, Vector2.zero);

        foreach (var hit in hits)
        {
            var drop = hit.collider.GetComponent<DropCables>();
            if (drop != null)
            {
                if (drop.CheckCable(currentDragging.gameObject,currentDragging.itemType))
                {
                    currentDragging.assignedSlot = true;
                    CableLineManager.Instance.LeaveActiveCable();
                }
                break;
            }
        }
        if (currentDragging.assignedSlot == false)
        {
            currentDragging.transform.position = currentDragging.startPos;
        }

        currentDragging = null;
    }
}
