using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalDisplayManager : MonoBehaviour
{
    [SerializeField] GameObject[] crystalObjects;
    [SerializeField] Renderer portalRenderer; 
    [SerializeField] Material[] portalMaterials;

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

        if (count < portalMaterials.Length)
        {
            portalRenderer.material = portalMaterials[count];
        }
    }
}
