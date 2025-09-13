using DG.Tweening;
using UnityEngine;

public class TapEffect_Enlarge : MonoBehaviour, ITapEffect
{
    public Transform target;
    public float toScale = 1.1f;
    public float duration = 0.2f;
    public Ease ease = Ease.OutBack;

    void Reset() { if (!target) target = transform.parent.parent; }

    public Tween Play()
    {
        if (!target) return null;
        target.DOKill(true);
        var t = target.DOScale(Vector3.one * toScale, duration).SetEase(ease).SetId(target.GetInstanceID());
        //var t2 = target.DOScale(Vector3.one * toScale, duration).SetEase(ease).SetId("enlarge" + target.GetInstanceID());
        return target.DOScale(Vector3.one * toScale, duration).SetEase(ease).SetId("enlarge" + target.GetInstanceID());
    }
}
