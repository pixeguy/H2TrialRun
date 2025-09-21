using UnityEngine;

public class ActualItemCover : MonoBehaviour
{
    private DropGameObject slot;
    public GameObject child;
    public bool occupied = false;

    private void OnEnable()
    {
        slot = GetComponentInParent<DropGameObject>();
        slot.objectDropped += PlayEffect;
    }
    private void OnDisable()
    {
        slot.objectDropped -= PlayEffect;
    }
    public void PlayEffect(GameObject var)
    {
        occupied = true;
        child.SetActive(true);
    }
}
