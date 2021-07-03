using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField]int index;
    private bool haveBeenPressed = false;
    public void moveLevelButtons()
    {
      if(haveBeenPressed == false)
      {
        LeanTween.scale(gameObject,new Vector3(1,1,1), 0.1f);
        LeanTween.move(gameObject,transform.position - new Vector3(500,0,0), 0.1f);
      }
      haveBeenPressed = true;
    }

    public void loadLevel()
    {
      SceneManager.LoadScene(sceneBuildIndex:index);
    }

}
