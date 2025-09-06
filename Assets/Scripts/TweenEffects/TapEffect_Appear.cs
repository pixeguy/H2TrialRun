using DG.Tweening;
using UnityEngine;

public class TapEffect_Appear : MonoBehaviour, ITapEffect
{
    public Transform target;
    [Header("Scale -> 1")]
    public float scaleUpTime = 0.25f;
    public Ease scaleEase = Ease.OutBack;
    public bool startFromZero = false;   // optional

    [Header("Punch")]
    public float punchAmount = 0.08f;
    public float punchTime = 0.18f;
    public int vibrato = 8;
    public float elasticity = 1f;

    void Reset() { if (!target) target = transform.parent.parent; }

    public Tween Play()
    {
        if (!target) return null;

        target.DOKill();
        if (startFromZero) target.localScale = Vector3.zero;

        return DOTween.Sequence()
            .Append(target.DOScale(1f, scaleUpTime).SetEase(scaleEase))
            .Append(target.DOPunchScale(Vector3.one * punchAmount, punchTime, vibrato, elasticity));
    }
}
