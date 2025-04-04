using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float flashDuration = 0.1f;
    private int currentHealth;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        currentHealth = maxHealth;
    }
    private void Update()
    {
        //transform.position += Vector3.left * speed * Time.deltaTime;

        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            //skibidi code
            // Vector2 movement = (player.position - transform.position).normalized * speed * Time.deltaTime;
            //transform.position += (Vector3)movement + (Vector3.left * LevelScroller.speed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
         currentHealth -= damage;
        StartCoroutine(FlashRed());
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
     private System.Collections.IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        LevelManager.Instance.EnemyKilled();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerShooter player = other.GetComponent<PlayerShooter>();
            if (player != null)
            {
                player.TakeDamage(15f);
            }
            Destroy(gameObject);
        }
    }

}
