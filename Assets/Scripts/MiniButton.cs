using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class MiniButton : MonoBehaviour
{

  public int idMini = 1;
  GameController gc;





  public void selfDistruct()
  {
    gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    gc.listEdit(idMini);
    Object.Destroy(this.gameObject);
  }
  public void des()
  {
    Object.Destroy(this.gameObject);
  }

  void Update()
  {
    if(CrossPlatformInputManager.GetButtonDown("Left"))
    {
      Object.Destroy(this.gameObject);
    }
    if(CrossPlatformInputManager.GetButtonDown("Right"))
    {
      Object.Destroy(this.gameObject);
    }
    if(CrossPlatformInputManager.GetButtonDown("Move"))
    {
      Object.Destroy(this.gameObject);
    }
    if(CrossPlatformInputManager.GetButtonDown("Attack"))
    {
      Object.Destroy(this.gameObject);
    }
    if(CrossPlatformInputManager.GetButtonDown("ds"))
    {
      Object.Destroy(this.gameObject);
    }
  }




}
