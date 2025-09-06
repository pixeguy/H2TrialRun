using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TapToInteractObject : InteractibleObject, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public UnityEvent interactAction;
    public TapEffectKey onTap;
    public TapEffectKey onHold;
    public TapEffectKey onRelease;
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
    public void AppearAction()
    {
        Sequence seq = DOTween.Sequence()
         .Append(transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack))
         .Append(transform.DOPunchScale(Vector3.one * 0.08f, 0.18f, 8, 1));
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canInteract) return;
        Play(onTap);
        interactAction.Invoke();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("checking");
        if (!canInteract) return;
        Debug.Log("canInteract");
        Play(onHold);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!canInteract) return;

        Play(onRelease);
    }

    public override void Interact()
    {
    }
}
