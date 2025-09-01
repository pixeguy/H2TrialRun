using TMPro;
using UnityEngine;

public class TrackItemCounterInSR : MonoBehaviour
{
    public Item itemType;
    public int itemCount;
    public TextMeshProUGUI textCount;

    private void Update()
    {
        textCount.text = itemCount.ToString();
    }

    public void addCount()
    {
        itemCount++;
    }

    public void deductCount()
    {
        if (itemCount > 0)
        {
            itemCount--;
        }
    }
}
