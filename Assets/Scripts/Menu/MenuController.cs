using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject menuPanel;

    void Start()
    {
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        Debug.Log("Вихід з гри");
        Application.Quit();
    }
    
    public void GoToIsland()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("IslandStart");
    }

    public void GameSettingsOpen()
    {
        settingPanel.SetActive(true);
        menuPanel.SetActive(false);
    }
    public void GameSettingsClose()
    {
        settingPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
