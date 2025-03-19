using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsGenerator : MonoBehaviour
{
    public GameObject platform;
    void Start()
    {
        Vector3 SpawnerPosition = new Vector3();

        for (int i = 0; i < 12; i++)
        {
            SpawnerPosition.x = Random.Range(-3f, 3f);
            SpawnerPosition.y = Random.Range(1f,50f);
            Instantiate(platform, SpawnerPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
