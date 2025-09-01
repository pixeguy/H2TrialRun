using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TapToInteractObject : InteractibleObject
{
    public UnityEvent interactAction;
    public override void Interact()
    {
    }
    void OnMouseUpAsButton()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;
        Interact();
        interactAction.Invoke();
    }
}
