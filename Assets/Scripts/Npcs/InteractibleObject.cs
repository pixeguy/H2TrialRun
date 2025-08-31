using UnityEngine;

public abstract class InteractibleObject : MonoBehaviour
{
    public abstract void Interact();
    void OnDrawGizmos()
    {
        CircleCollider2D cc = GetComponent<CircleCollider2D>();
        if (cc != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + (Vector3)cc.offset, cc.radius);
        }
    }
}
