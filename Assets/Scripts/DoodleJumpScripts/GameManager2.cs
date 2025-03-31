using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject pausePanel;
    private bool isPaused = false;
    private int platformCount = 0;
    private int winThreshold = 30;

    void Update()
        {
             if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    public void AddPlatform()
    {
        platformCount++;
        if (platformCount >= winThreshold)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Time.timeScale = 0;
        Panel.SetActive(true);
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

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("IslandStart");
    }
}
