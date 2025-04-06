using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
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
