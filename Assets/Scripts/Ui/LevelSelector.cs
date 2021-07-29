using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public GameObject[] levelButtons;
    int levelsUnloked = 1;
    int index;
    void Awake()
    {
      levelButtons = GameObject.FindGameObjectsWithTag("levelButton");
    }
    void Start()
    {
      levelsUnloked = PlayerPrefs.GetInt("levelsUnloked");
    }



}
