using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrystalManager : MonoBehaviour
{
    public static void CollectCrystal(int id)
    {
        PlayerPrefs.SetInt($"Crystal{id}", 1);
        PlayerPrefs.Save();
    }

    public static bool IsCrystalCollected(int id)
    {
        return PlayerPrefs.GetInt($"Crystal{id}", 0) == 1;
    }

    public static int TotalCollectedCrystals()
    {
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            if (IsCrystalCollected(i)) count++;
        }
        return count;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save(); //потім видалити ресет
            Debug.Log("Всі кристали обнулено!");
        }
    }
    public static bool AreAllCrystalsCollected()
    {
        return TotalCollectedCrystals() == 4;
    }
    public void TryActivatePortal()
    {
        if (AreAllCrystalsCollected())
        {
            SceneManager.LoadScene("End");
        }
    }
}
