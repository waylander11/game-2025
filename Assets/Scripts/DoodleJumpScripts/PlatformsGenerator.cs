using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsGenerator : MonoBehaviour
{
    public GameObject platform;
    void Start()
    {
        Vector3 SpawnerPosition = new Vector3();

        for (int i = 0; i < 10; i++)
        {
            SpawnerPosition.x = Random.Range(-2.5f, 3f);
            SpawnerPosition.y = Random.Range(1f,40f);
            Instantiate(platform, SpawnerPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
}
