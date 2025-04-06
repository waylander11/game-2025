using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrystalDisplayManager : MonoBehaviour
{
    [SerializeField] GameObject[] crystalObjects;
    [SerializeField] GameObject[] teleporters;
    [SerializeField] GameObject pausePanel;
    private bool isPaused = false;
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }
    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
