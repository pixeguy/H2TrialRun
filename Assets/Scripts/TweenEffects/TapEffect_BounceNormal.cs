using DG.Tweening;
using UnityEngine;

public class TapEffect_BounceNormal : MonoBehaviour, ITapEffect
{
    public Transform target;
    public float punchAmount = 0.2f;
    public float duration = 0.3f;
    public int vibrato = 10;
    public float elasticity = 1f;
    public Ease ease = Ease.InOutQuad;

    void Reset() { if (!target) target = transform.parent.parent; }

    public Tween Play()
    {
        if (!target) return null;
        target.DOKill(true);

        target.localScale = Vector3.one;
        return target.DOPunchScale(Vector3.one * punchAmount, duration, vibrato, elasticity).SetId(target.GetInstanceID());
    }
}
