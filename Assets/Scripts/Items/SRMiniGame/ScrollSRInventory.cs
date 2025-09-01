using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollSRInventory : MonoBehaviour
{
    [SerializeField] Transform content;      // child that actually scrolls
    [SerializeField] float scrollSpeed = 2000f;

    float targetX;
    Coroutine scrollCo;

    public void ScrollLeft()
    {
        targetX = content.position.x - 1920f;
        if (scrollCo != null) StopCoroutine(scrollCo);
        scrollCo = StartCoroutine(ScrollToX());
    }

    IEnumerator ScrollToX()
    {
        while (Mathf.Abs(content.position.x - targetX) > 0.5f)
        {
            float nx = Mathf.MoveTowards(content.position.x, targetX, scrollSpeed * Time.deltaTime);
            content.position = new Vector3(nx, content.position.y, content.position.z);
            yield return null;
        }
        content.position = new Vector3(targetX, content.position.y, content.position.z);
        scrollCo = null;
    }
}
