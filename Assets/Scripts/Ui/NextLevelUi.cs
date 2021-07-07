using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelUi : MonoBehaviour
{
  public GameObject NextLevelEllement;
  private float gameSpeed = 1f;




  public void RestartLevel()
  {
    Time.timeScale = gameSpeed;

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void ActivateUi()
  {
    Time.timeScale = 0;
    NextLevelEllement.SetActive(true);


  }
  
  public void goToMainMenu()
  {
    Time.timeScale = gameSpeed;
    SceneManager.LoadScene(sceneBuildIndex:0);
  }
  public void loadNextLevel()
  {
    Time.timeScale = gameSpeed;

    Scene scene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(sceneBuildIndex:scene.buildIndex+1);
  }

}
