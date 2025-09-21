using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
[Serializable]
public class ActivationRule
{
    public UnityEvent func;
    public float activateDelay;
}
public class SceneControl : MonoBehaviour
{
    public ActivationRule[] rules;

    void Start()
    {
        foreach (ActivationRule rule in rules)
        {
            StartCoroutine(Activate(rule));
        }
    }
    private IEnumerator Activate(ActivationRule rule)
    {
        yield return new WaitForSeconds(rule.activateDelay);
        rule.func.Invoke();
    }
}
