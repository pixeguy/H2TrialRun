using UnityEngine;

public class ErrorManager : MonoBehaviour
{
    public static ErrorManager instance;
    [SerializeField] private ErrorText error;
    public Transform canvas;

    private void Awake()
    {
        instance = this;
    }

    public void Init(string message)
    {
        var errorObj = Instantiate(error,canvas);
        errorObj.SetErrorMessage(message);
    }
}
