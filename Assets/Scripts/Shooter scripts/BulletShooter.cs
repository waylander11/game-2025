using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
 
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private int damage = 1;
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag("Enemy"))
        {
            EnemyShooter enemy = other.GetComponent<EnemyShooter>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
