using System.Collections.Generic;
using UnityEngine; // for Unity 'null' checks on UnityEngine.Object

public static class ReturnBus
{
    // per-Item queue of TapToDisappear sources
    private static readonly Dictionary<Item, Queue<TapToDisappear>> _q = new();

    public static void Enqueue(Item key, TapToDisappear src)
    {
        if (!key || !src) return;
        if (!_q.TryGetValue(key, out var queue)) _q[key] = queue = new Queue<TapToDisappear>();
        queue.Enqueue(src);
    }

    public static TapToDisappear Dequeue(Item key)
    {
        if (!_q.TryGetValue(key, out var queue) || queue.Count == 0) return null;
        return queue.Dequeue();
    }

    public static void ReturnN(Item key, int n)
    {
        if (!_q.TryGetValue(key, out var queue)) return;
        for (int i = 0; i < n && queue.Count > 0; i++)
        {
            var t = queue.Dequeue();
            if (t) t.AppearAction();
        }
    }

    public static void ReturnAll(Item key)
    {
        if (!_q.TryGetValue(key, out var queue)) return;
        while (queue.Count > 0)
        {
            var t = queue.Dequeue();
            if (t) t.AppearAction();
        }
    }
}