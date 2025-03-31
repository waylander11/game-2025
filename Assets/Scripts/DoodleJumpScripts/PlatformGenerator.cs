using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] GameObject islandPrefab;
    [SerializeField] Transform player;
    [SerializeField] GameManager gameManager;
    private float spawnHeight = 5f;
    private float lastSpawnY;

    void Start()
    {
        lastSpawnY = player.position.y;
    }

    void Update()
    {
        if (player.position.y + spawnHeight > lastSpawnY)
        {
            SpawnIsland();
        }
    }

    void SpawnIsland()
    {
        float randomX = Random.Range(-2.5f, 3f);
        lastSpawnY += spawnHeight;
        Vector3 spawnPosition = new Vector3(randomX, lastSpawnY, 0);
        Instantiate(islandPrefab, spawnPosition, Quaternion.identity);
    }
}

