using UnityEngine;

public class UIOnEnableEffect : MonoBehaviour
{
    [Header("Pair with TapEffect")]
    private ITapEffect effect;
    public bool PlayOnEnable = true;
    private void OnEnable()
    {
        if (PlayOnEnable)
        {
            PlayEffect();
        }
    }
    public void PlayEffect()
    {
        effect = GetComponent<ITapEffect>();
        effect.Play();
    }
}
