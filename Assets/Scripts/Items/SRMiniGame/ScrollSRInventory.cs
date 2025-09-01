using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollSRInventory : MonoBehaviour
{
    [SerializeField] RectTransform content;      // child that actually scrolls
    [SerializeField] float scrollSpeed = 2000f;
    public int maxPages;

    private int contentPageIndex = 0;
    float targetX;
    Coroutine scrollCo;

    private void Update()
    {
    }

    public void ScrollLeft()
    {
        if (contentPageIndex <= 0) return;
        contentPageIndex -= 1;
        targetX = (-1920f * contentPageIndex);
        if (scrollCo != null) StopCoroutine(scrollCo);
        scrollCo = StartCoroutine(ScrollToX());
    }

    public void ScrollRight()
    {
        if (contentPageIndex >= maxPages - 1) return;
        contentPageIndex += 1;
        targetX = (-1920f * contentPageIndex);
        if (scrollCo != null) StopCoroutine(scrollCo);
        scrollCo = StartCoroutine(ScrollToX());
    }

    public void ResetScroll()
    {
        contentPageIndex = 0;
        targetX = 0f;
        if (scrollCo != null) StopCoroutine(scrollCo);
        scrollCo = StartCoroutine(ScrollToX());
    }

    IEnumerator ScrollToX()
    {
        while (Mathf.Abs(content.anchoredPosition.x - targetX) > 0.5f)
        {
            float nx = Mathf.MoveTowards(content.anchoredPosition.x, targetX, scrollSpeed * Time.unscaledDeltaTime);
            content.anchoredPosition = new Vector2(nx, content.anchoredPosition.y);
            yield return null;
        }
        content.anchoredPosition = new Vector2(targetX, content.anchoredPosition.y);
        scrollCo = null;
    }
}
