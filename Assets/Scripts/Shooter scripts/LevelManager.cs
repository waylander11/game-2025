using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
 private void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("IslandStart"); // Повернення на острів
        }
    }
    


    /*
     [SerializeField] private float scrollSpeed = 2f; // Швидкість прокрутки рівня
    [SerializeField] private Transform background; // Фон, який буде рухатися
    [SerializeField] private GameObject[] enemies; // Масив ворогів для спавну
    [SerializeField] private Transform enemySpawnPoint; // Точка появи ворогів
    [SerializeField] private float enemySpawnRate = 2f; // Частота появи ворогів

    private float spawnTimer = 0f;

    private void Update()
    {
        MoveBackground();
        HandleEnemySpawning();
        HandleExit();
    }

    private void MoveBackground()
    {
        if (background != null)
        {
            background.position += Vector3.left * scrollSpeed * Time.deltaTime;
        }
    }

    private void HandleEnemySpawning()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= enemySpawnRate)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (enemies.Length == 0 || enemySpawnPoint == null) return;

        int randomIndex = Random.Range(0, enemies.Length);
        GameObject enemy = Instantiate(enemies[randomIndex], enemySpawnPoint.position, Quaternion.identity);
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-scrollSpeed, 0);
    }

    private void HandleExit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("IslandStart"); // Назва сцени острова
        }
    }
    */
}
