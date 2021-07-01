using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    public int theNumber = 1;
    GameController gc;


    public void activatePanel()
    {
      gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
      gc.currentTab = theNumber;
      
    }
}
