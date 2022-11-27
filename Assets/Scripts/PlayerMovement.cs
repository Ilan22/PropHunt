using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 50f;
    public float acceleration = 50f;
    public float decceleration = 50f;
    public float velPower;
    public Rigidbody2D rb;
    public Transform groundCheck;
    [SerializeField] private LayerMask groundLayer; 
    public float jumpForce;

    public PolygonCollider2D polygon;

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        float targetSpeed = moveInput * speed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);

        if (Physics2D.BoxCast(polygon.bounds.center, polygon.bounds.size, 0f, Vector2.down,.1f,  groundLayer) && Input.GetButton("Jump"))
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        // coyote time ?
    }
}
