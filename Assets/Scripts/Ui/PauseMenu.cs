using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUi;
    private float gameSpeed = 1f;

    void Start ()
      {
        pauseMenuUi.SetActive(false);
        if(Time.timeScale == 0f)
        {
          Time.timeScale = gameSpeed;
        }
      }

    void Update()
    {
      if(CrossPlatformInputManager.GetButtonDown("Pause"))
      {
          if(GameIsPaused)
          {
            Resume();
          }
          else
          {
            Pause();
          }
      }
      if(CrossPlatformInputManager.GetButtonDown("Resume"))
      {
        Resume();
      }
    }

    public void Resume()
    {
      pauseMenuUi.SetActive(false);
      Time.timeScale = gameSpeed;
      GameIsPaused = false;
    }

    void Pause()
    {
      pauseMenuUi.SetActive(true);
      Time.timeScale = 0f;
      GameIsPaused = true;
    }

    public void goToMainMenu()
    {
      GameIsPaused = false;
      Time.timeScale = gameSpeed;
      SceneManager.LoadScene(sceneBuildIndex:0);
    }
    public void setGameSpeed()
    {
      if(gameSpeed == 1f)
      {
        gameSpeed = 2f;
      }
      else
      {
        gameSpeed = 1f;
      }

      Time.timeScale = gameSpeed;
    }
}
