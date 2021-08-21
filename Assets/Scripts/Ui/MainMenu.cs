using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{


  private List<int> order = new List<int>();
  private List<int> orderFOne = new List<int>();
  private List<int> orderFTwo = new List<int>();

    void Awake()
    {
      PlayerPrefsExtra.SetList("order", order);
      PlayerPrefsExtra.SetList("orderFOne", orderFOne);
      PlayerPrefsExtra.SetList("orderFTwo", orderFTwo);
      PlayerPrefs.SetInt("isExecutingNeeded", 0);
      Time.timeScale = 1f;
      PlayerPrefs.SetInt("levelsUnloked", 99);
    }


}
