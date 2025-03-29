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
    public PuzzleGenerator PuzzleGenerator;
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
            Debug.Log("Time left: " + timer);
        }
    }
    void CheckLoseCondition()
    {
        if (PuzzleGenerator.sliderValue <= 0 && timer > 0)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Update()
    {
        CheckLoseCondition();
    }
}
