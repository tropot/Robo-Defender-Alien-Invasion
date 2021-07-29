using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void closeProgram()
    {
      FindObjectOfType<AudioManager>().Play("Click");
      Application.Quit();
    }
}
