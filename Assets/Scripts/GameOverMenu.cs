using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    GameObject gameOverMenu;

    // Start is called before the first frame update
    void Awake()
    {
        gameOverMenu = transform.GetChild(0).gameObject;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
      
    }

    public void Restart()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
