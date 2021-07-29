using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class MiniButton : MonoBehaviour
{

  public int idMini;
  public int gridOnWitchSpawned;
  GameController gc;
  public GameObject currentImage;


  void Start()
  {
    LeanTween.scale(gameObject,new Vector3(1,1,1), 0.1f);
    if(idMini == 0)
    {
      currentImage.SetActive(true);
    }
  }



  public void selfDistruct()
  {
    FindObjectOfType<AudioManager>().Play("Click");
    gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    gc.listEdit(idMini);
    gc.matchGrid(gc.currentTab,0);
  }
  public void des()
  {
      Object.Destroy(this.gameObject);
  }

  public void currentAction(int index,int grid)
  {




    if(index == idMini & grid == gridOnWitchSpawned)
    {
      currentImage.SetActive(true);
    }
    else
    {
      currentImage.SetActive(false);
    }


  }








}
