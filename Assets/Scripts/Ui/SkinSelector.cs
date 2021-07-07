using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    public Sprite[] baseSprites;
    public Sprite[] towerSprites;
    public GameObject baseComponent;
    public GameObject towerSprite;
    private int currentBaseSprite = 0;
    private int currentTower = 0;

    void Start()
    {
      currentBaseSprite = PlayerPrefs.GetInt("currentBaseSprite");
      currentTower = PlayerPrefs.GetInt("currentTower");
      SetTower(currentTower);
      SetSprite(currentBaseSprite);
    }

    void SetSprite(int index)
    {
      baseComponent.GetComponent<Image> ().sprite = baseSprites[index];
    }
    void SetTower(int index)
    {
      towerSprite.GetComponent<Image> ().sprite = towerSprites[index];
    }
    public void NextRSprite()
    {
      currentBaseSprite += 1;
      if(currentBaseSprite >= baseSprites.Length)
      {
        currentBaseSprite = 0;
      }
      SetSprite(currentBaseSprite);
      PlayerPrefs.SetInt("currentBaseSprite", currentBaseSprite);

    }
    public void NextLSprite()
    {

      if(currentBaseSprite == 0)
      {
        currentBaseSprite =  baseSprites.Length - 1;
      }
      else
      {
        currentBaseSprite -= 1;
      }
      SetSprite(currentBaseSprite);
      PlayerPrefs.SetInt("currentBaseSprite", currentBaseSprite);

    }
    public void NextRTower()
    {
      currentTower += 1;
      if(currentTower >= towerSprites.Length)
      {
        currentTower = 0;
      }
      SetTower(currentTower);
      PlayerPrefs.SetInt("currentTower", currentTower);

    }
    public void NextLTower()
    {

      if(currentTower == 0)
      {
        currentTower =  towerSprites.Length - 1;
      }
      else
      {
        currentTower -= 1;
      }
      SetTower(currentTower);
      PlayerPrefs.SetInt("currentTower", currentTower);

    }

}
