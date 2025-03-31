using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodleMovement : MonoBehaviour
{
    private float speed = 5f;
    private float jumpForce = 10f;
    float moveInput;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update() 
    {
        moveInput = Input.GetAxisRaw("Horizontal");
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y); 
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("Touching: ");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
