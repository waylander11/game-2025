using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public static PlayerShooter Instance;
    private EnemyShooter EnemyShooter; 
    [SerializeField] GameObject losePanel;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float maxHealth = 100f;
     private float currentHealth;
    private bool facingRight = true;

    private Rigidbody2D rb;
    private Vector2 movement;

private void Start()
    {
       UIManager.Instance.UpdatePlayerHealth(currentHealth);
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        
    }

    private void Update()
    {
        MoveInput();
        if (Input.GetMouseButtonDown(0)) // ЛКМ - стрільба
        {
            Shoot();
        }
         // Дзеркалимо спрайт гравця
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void MoveInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        rb.velocity = movement * moveSpeed;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UIManager.Instance.UpdatePlayerHealth(currentHealth);
        
        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
            losePanel.SetActive(true);
        }
    }

    private void Shoot()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)shootPoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        bulletRb.velocity = direction * bulletSpeed;
        bullet.transform.right = direction; // Повертаємо кулю в сторону миші
        
        Destroy(bullet, 2f);
    }
private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        
    }
}
