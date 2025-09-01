using UnityEngine;
using UnityEngine.Events;

public class Npc1 : InteractibleObject
{
    public UnityEvent interactAction;
    public override void Interact()
    {
        if(Input.GetKey(KeyCode.E))
        {
            interactAction.Invoke();
        }
    }
}
