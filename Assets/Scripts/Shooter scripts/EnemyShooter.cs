using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int health = 3;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject); // Вбиваємо гравця (пізніше можна замінити на систему HP)
            Destroy(gameObject); 
        }
    }

}
