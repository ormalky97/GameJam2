using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Background")]
    public RawImage background;
    public float xFactor;
    public float yFactor;

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        float newX = background.uvRect.x + xFactor * Time.deltaTime;
        float newY = background.uvRect.y + yFactor * Time.deltaTime;
        background.uvRect = new Rect(newX, newY, background.uvRect.width, background.uvRect.height);
    }
}
