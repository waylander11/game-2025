using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject winPanel;
    private int enemiesKilled = 0;
    private int targetKills = 25;
    private bool isPaused = false;
    private void Start()
    {
       UIManager.Instance.UpdateKillsCounter(enemiesKilled, targetKills);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    void TogglePause()
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToIsland()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("IslandStart");
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        UIManager.Instance.UpdateKillsCounter(enemiesKilled, targetKills);
        
    }
    
    
}
