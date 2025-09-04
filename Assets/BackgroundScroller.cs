using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private Vector2 textureOffset = Vector2.zero;
    private float speed = 0.1f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        textureOffset.x += speed * Time.deltaTime;
        textureOffset.y += speed * Time.deltaTime;
        spriteRenderer.material.mainTextureOffset = textureOffset;
    }
}
