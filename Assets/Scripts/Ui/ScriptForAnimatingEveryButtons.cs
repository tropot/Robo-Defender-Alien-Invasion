using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScriptForAnimatingEveryButtons : MonoBehaviour
{



    public void Disapier()
    {
      LeanTween.scale(gameObject,new Vector3(0,0,0), 0.5f).setOnComplete(DestroyMe);
    }
    private void DestroyMe()
    {
      Destroy(gameObject);
    }
    public void menu()
    {
      SceneManager.LoadScene(sceneBuildIndex:0);
    }
    public void levelSelect()
    {
      SceneManager.LoadScene("LevelSelector");
    }


}
