using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoodleMovement : MonoBehaviour
{
    public float speed = 5f;
    float moveInput;
    public static DoodleMovement instance;
    public Rigidbody2D rb;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        rb = GetComponent<Rigidbody2D>();
    }
    void Update() 
    {
        moveInput = Input.GetAxisRaw("Horizontal");
    }
    void FixetUpdate()
    {
        rb.velocity = new Vector2(moveInput * speed,rb.velocity.y); 
    }
    public void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.collider.name == "Cleaner")
        {
            SceneManager.LoadScene(0);
        }
    }
}
