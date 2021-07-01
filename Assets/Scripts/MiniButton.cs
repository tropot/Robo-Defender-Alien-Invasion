using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class MiniButton : MonoBehaviour
{

  public int idMini = 1;
  GameController gc;

  void Start()
  {
    LeanTween.scale(gameObject,new Vector3(1,1,1), 0.1f);
  }



  public void selfDistruct()
  {
    gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    gc.listEdit(idMini);
    gc.matchGrid(gc.currentTab,0);
  }
  public void des()
  {
      Object.Destroy(this.gameObject);

  }








}
