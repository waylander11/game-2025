using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [Header("Player UI")]
    public Slider playerHealthSlider;
    //public TextMeshProUGUI killsCounterText;
    public Text killsCounterText;
    
    [Header("Game Win")]
    [SerializeField] GameObject winPanel;

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

    public void UpdatePlayerHealth(float currentHealth)
    {
        playerHealthSlider.value = currentHealth;
    }

    public void UpdateKillsCounter(int kills, int targetKills)
    {
        killsCounterText.text = $"{kills}/{targetKills}";
        if (kills >= 25)
        {
            CrystalManager.CollectCrystal(2);
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }
    }
}
