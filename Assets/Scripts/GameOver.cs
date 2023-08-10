using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    
    public void GameOverCanvas()
    {
        
        pauseMenuUI.SetActive(true);
        Time.timeScale  = 0;
        Cursor.lockState = CursorLockMode.None;
        GameIsPaused = true;
        AudioListener.pause = true;

    }
}
