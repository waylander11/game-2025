using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CrystalManager.CollectCrystal(1);
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
