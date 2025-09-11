using TMPro;
using UnityEngine;

public class ErrorText : MonoBehaviour
{
    private string errorMessage;
    private TextMeshProUGUI errorText;

    public void SetErrorMessage(string message)
    {
        errorMessage = message;
        errorText = GetComponentInChildren<TextMeshProUGUI>();
        errorText.text = errorMessage;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
