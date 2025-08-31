using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;
    public float speed = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Only allow movement in one axis at a time
        if (horizontalInput != 0)
            verticalInput = 0;

        float horizontalSpeed = horizontalInput * speed;
        float verticalSpeed = verticalInput * speed;

        rb.linearVelocity = new Vector2(horizontalSpeed, verticalSpeed);

    }
}
