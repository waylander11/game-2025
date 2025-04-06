using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CrystalManager crystalManager = FindObjectOfType<CrystalManager>();
            if (crystalManager != null)
            {
                Debug.Log("Всі кристали зібрано! Завантаження сцени...");
                crystalManager.TryActivatePortal();
            }
            else
            {
                Debug.Log("Не всі кристали зібрано! Портал не активний.");
            }
        }
    }   
}
