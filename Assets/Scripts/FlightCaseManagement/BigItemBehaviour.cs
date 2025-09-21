using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BigItemBehaviour : MonoBehaviour
{
    private Collider2D collider;
    public UnityEvent AppearBackEvent;
    public TapEffectKey AppearEffect;
    Dictionary<TapEffectKey, List<TapEffectGroup>> map;

    void Awake()
    {
        map = new Dictionary<TapEffectKey, List<TapEffectGroup>>();
        foreach (var g in GetComponentsInChildren<TapEffectGroup>(true))
        {
            if (!g.key) continue;
            if (!map.TryGetValue(g.key, out var list)) map[g.key] = list = new List<TapEffectGroup>();
            list.Add(g);
        }
    }
    public Tween Play(TapEffectKey key)
    {
        if (!key || !map.TryGetValue(key, out var list)) return null;
        var seq = DOTween.Sequence();
        foreach (var g in list) { var t = g.Play(); if (t != null) seq.Join(t); }
        return seq;
    }

    public void EnqueueItemBus(Item itemType)
    {
        ReturnBus.Enqueue(itemType,this);
    }

    public void AppearAction()
    {
        Play(AppearEffect);
        AppearBackEvent.Invoke();
    }
}
