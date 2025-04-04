using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
   [SerializeField] private float speed = 3f;
    private Rigidbody2D rb;
    private Animator animator;
    private float moveX;
    private float moveY;
    private string lastAnimation = "Walk_Down"; //тут початкова анімація

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0 || moveY != 0)
        {
            // тут напряиок руху
            if (Mathf.Abs(moveX) > Mathf.Abs(moveY)) 
            {
                lastAnimation = (moveX > 0) ? "Walk_Right" : "Walk_Left";
            }
            else 
            {
                lastAnimation = (moveY > 0) ? "Walk_Up" : "Walk_Down";
            }

            animator.Play(lastAnimation);
        } // тут виклик анімації
            else
            {
            animator.speed = 0f; // Зупиняє анімацію
            }
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX * speed, moveY * speed);
    }
    void LateUpdate()
    {
        // Вмикаємо назад анімацію, якщо гравець знову рухається
        if (moveX != 0 || moveY != 0)
        {
            animator.speed = 1f; 
        }
    }
}
