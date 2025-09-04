using DG.Tweening;
using UnityEngine;

public abstract class InteractibleObject : MonoBehaviour
{
    [HideInInspector]
    public bool canInteract = true;
    public abstract void Interact();
    public void BounceAction()
    {
        transform.DOKill();
        transform.localScale = Vector3.one;
        transform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 10, 1);
    }
}
