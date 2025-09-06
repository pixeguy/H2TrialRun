using DG.Tweening;
using UnityEngine;

public class TapEffect_Disappear : MonoBehaviour, ITapEffect
{
    public Transform target;
    public float punch = 0.2f;
    public float punchTime = 0.2f;
    public float shrinkTime = 0.25f;

    void Reset() { if (!target) target = transform.parent.parent; }

    public Tween Play()
    {
        if (!target) return null;
        target.DOKill();
        target.localScale = Vector3.one;

        var seq = DOTween.Sequence();
        seq.Append(target.DOPunchScale(Vector3.one * punch, punchTime, 10, 1));
        seq.Append(target.DOScale(0f, shrinkTime).SetEase(Ease.InBack));
        // optional: disable at end
        // seq.OnComplete(() => target.gameObject.SetActive(false));
        return seq;
    }
}
