using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerShooter : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private float spawnDistance = 10f; 
    [SerializeField] private float screenPadding = 2f; // Запас для відстані від країв екрану, поки не використана

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnRate);
    }

    private void SpawnEnemy()
    {
        //  межі екрану
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Випадково  одну з чотирьох сторін за екраном, може краще з двох зробити
        Vector3 spawnPosition = new Vector3(
            Random.Range(screenBottomLeft.x - spawnDistance, screenTopRight.x + spawnDistance),
            Random.Range(screenBottomLeft.y - spawnDistance, screenTopRight.y + spawnDistance),
            0f
        );

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);


    }




/*  
//Код для спавнпоінтів 

  [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnRate = 3f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnRate);
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
*/


  
}
