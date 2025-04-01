using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text timerText;
    private float timer = 60f;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject winPanel;
    private bool isPaused = false;
    public PuzzleGenerator PuzzleGenerator;
    public PuzzleGenerator sliderValue;
    void Start()
    {
        losePanel.SetActive(false);
        StartCoroutine(TimerCountdown());
    }
    IEnumerator TimerCountdown()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
            timerText.text = "Time: " + timer;
        }
    }
    void CheckLoseCondition()
    {
        if (PuzzleGenerator.sliderValue <= 0 && timer > 0)
        {
            PuzzleGenerator.enabled = false;
            losePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    void CheckWinCondition()
    {
        if (timer <= 0 && PuzzleGenerator.sliderValue >= 0)
        {
            PuzzleGenerator.enabled = false;
            winPanel.SetActive(true);
            Time.timeScale = 0;
            
        }
    }
    void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PuzzleGenerator.enabled = true;
    }
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        CheckLoseCondition();
        CheckWinCondition();
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            PuzzleGenerator.enabled = false;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            PuzzleGenerator.enabled = true;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }
    void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("IslandStart");
    }
}
