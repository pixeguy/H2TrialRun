using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CollisionTarget
{
    public LayerMask lm;
    public UnityEvent onCollisionEnter;
    public UnityEvent onCollisionExit;
    public UnityEvent onCollisionStay;
    //public UnityEvent onTriggerEnter;
    //public UnityEvent onTriggerExit;
}

public class CheckCollider : MonoBehaviour
{
    public CollisionTarget[] targets;
    public void OnCollisionEnter2D(Collision2D other)
    {
        foreach (CollisionTarget target in targets)
        {
            if (((1 << other.gameObject.layer) & target.lm.value) != 0)
            {
                target.onCollisionEnter.Invoke();
                return;
            }
        }
    }


    public void OnCollisionExit2D(Collision2D other)
    {
        foreach (CollisionTarget target in targets)
        {
            if (((1 << other.gameObject.layer) & target.lm.value) != 0)
            {
                target.onCollisionExit.Invoke();
                return;
            }
        }
    }
}
