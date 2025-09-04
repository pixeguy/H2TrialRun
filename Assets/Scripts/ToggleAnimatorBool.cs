using UnityEngine;

public class ToggleAnimatorBool : MonoBehaviour
{
    public void ToggleBool(string boolName)
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            bool currentValue = animator.GetBool(boolName);
            animator.SetBool(boolName, !currentValue);
        }
        else
        {
            Debug.LogWarning("No Animator component found on this GameObject.");
        }
    }
}
