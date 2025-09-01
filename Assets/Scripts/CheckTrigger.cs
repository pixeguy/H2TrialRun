using UnityEngine;
using UnityEngine.Events;


public class CheckTrigger : MonoBehaviour
{
    public CollisionTarget[] targets;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (CollisionTarget target in targets)
        {
            if (((1 << collision.gameObject.layer) & target.lm.value) != 0)
            {
                target.onCollisionEnter.Invoke();
                return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (CollisionTarget target in targets)
        {
            if (((1 << collision.gameObject.layer) & target.lm.value) != 0)
            {
                target.onCollisionExit.Invoke();
                return;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (CollisionTarget target in targets)
        {
            if (((1 << collision.gameObject.layer) & target.lm.value) != 0)
            {
                target.onCollisionStay.Invoke();
                return;
            }
        }
    }
}
