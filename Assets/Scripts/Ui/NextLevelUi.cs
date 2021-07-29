using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelUi : MonoBehaviour
{
  public GameObject NextLevelEllement;
  private float gameSpeed = 1f;
  public List<Commands> order = new List<Commands>();
  public List<Commands> orderFOne = new List<Commands>();
  public List<Commands> orderFTwo = new List<Commands>();


  public void RestartLevel()
  {
    FindObjectOfType<AudioManager>().Play("Click");
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
    FindObjectOfType<AudioManager>().Play("Click");
    Time.timeScale = gameSpeed;
    SceneManager.LoadScene(sceneBuildIndex:0);
  }
  public void loadNextLevel()
  {
    FindObjectOfType<AudioManager>().Play("Click");
    Time.timeScale = gameSpeed;
    PlayerPrefsExtra.SetList("order", order);
    PlayerPrefsExtra.SetList("orderFOne", orderFOne);
    PlayerPrefsExtra.SetList("orderFTwo", orderFTwo);
    Scene scene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(sceneBuildIndex:scene.buildIndex+1);
  }

}
