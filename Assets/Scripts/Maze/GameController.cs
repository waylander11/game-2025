using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private float moveX;
    private float moveY;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        moveX = Input.GetAxisRaw("Horizontal"); 
        moveY = Input.GetAxisRaw("Vertical");

        animator.SetFloat("moveX", moveX);
        animator.SetFloat("moveY", moveY);
        animator.SetFloat("Speed", Mathf.Abs(moveX) + Mathf.Abs(moveY));
    }

    void FixedUpdate()
    {
        rb.velocity = movement.normalized * speed;
    }
}
