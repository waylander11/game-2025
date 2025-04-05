using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalDisplayManager : MonoBehaviour
{
    [SerializeField] GameObject[] crystalObjects;
    [SerializeField] GameObject[] teleporters;
    [SerializeField] SpriteRenderer portalRenderer; 
    [SerializeField] Sprite[] portalSprites;

    void Start()
        {
            int count = 0;

            for (int i = 0; i < crystalObjects.Length; i++)
            {
                if (CrystalManager.IsCrystalCollected(i))
                {
                    crystalObjects[i].SetActive(true);
                    count++;
                }
            }

            if (count < portalSprites.Length)
            {
                portalRenderer.sprite = portalSprites[count];
            }
            
            for (int i = 0; i < teleporters.Length; i++)
            {
                teleporters[i].SetActive(i == count);
            }
        }
}
