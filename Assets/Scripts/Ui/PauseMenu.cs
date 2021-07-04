using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUi;


    void Start ()
      {
        pauseMenuUi.SetActive(false);
      }

    void Update()
    {
      if(CrossPlatformInputManager.GetButtonDown("Pause"))
      {
          if(GameIsPaused == false)
          {
            Pause();
          }
      }
    }

    public void Resume()
    {
      pauseMenuUi.SetActive(false);
      Time.timeScale = 1f;
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
      Time.timeScale = 1f;
      SceneManager.LoadScene(sceneBuildIndex:0);
    }
}
