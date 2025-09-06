// TapEffectGroup.cs
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TapEffectGroup : MonoBehaviour
{
    public TapEffectKey key;                 // drag a key asset here

    List<ITapEffect> _effects;

    void Awake()
    {
        _effects = new List<ITapEffect>();
        foreach (var mb in GetComponentsInChildren<MonoBehaviour>(true))
            if (mb is ITapEffect eff) _effects.Add(eff);
    }

    public Tween Play()
    {
        if (_effects == null || _effects.Count == 0) return null;
        var seq = DOTween.Sequence();
        foreach (var e in _effects)
        {
            var t = e.Play();
            if (t == null) continue;
            seq.Join(t);
        }
        return seq;
    }
}