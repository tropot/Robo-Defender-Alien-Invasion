using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    public Sprite[] baseSprites;
    public Tower[] towerSprites;
    public GameObject baseComponent;
    public GameObject towerSprite;
    private int currentBaseSprite = 0;
    private int currentTower = 0;
    int levelsUnloked;
    public Text txt;

    void Start()
    {
      levelsUnloked = PlayerPrefs.GetInt("levelsUnloked");
      currentBaseSprite = PlayerPrefs.GetInt("currentBaseSprite");
      currentTower = PlayerPrefs.GetInt("currentTower");
      foreach(Tower tover in towerSprites)
      {
        if(tover.levelsNeededToUnlock <= levelsUnloked)
        {
          tover.isUnlocked = true;
        }
      }
      SetTower(currentTower);
      SetSprite(currentBaseSprite);
    }

    void SetSprite(int index)
    {
      baseComponent.GetComponent<Image> ().sprite = baseSprites[index];
    }
    void SetTower(int index)
    {
      if(towerSprites[currentTower].isUnlocked == false)
      {
        towerSprite.GetComponent<Image> ().color = new Color32(0,0,0,255);

        txt.text = string.Format("{0}",towerSprites[currentTower].whatNeedsToBeDoneToUnlock);
      }
      else{
        towerSprite.GetComponent<Image> ().color = new Color32(255,255,255,255);
        PlayerPrefs.SetInt("currentTower", currentTower);
        txt.text = string.Format("Selected");
      }
      towerSprite.GetComponent<Image> ().sprite = towerSprites[index].sprite;
    }
    public void NextRSprite()
    {
      FindObjectOfType<AudioManager>().Play("Click");

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
      FindObjectOfType<AudioManager>().Play("Click");

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
      FindObjectOfType<AudioManager>().Play("Click");

      currentTower += 1;
      if(currentTower >= towerSprites.Length)
      {
        currentTower = 0;
      }
      SetTower(currentTower);

    }
    public void NextLTower()
    {
      FindObjectOfType<AudioManager>().Play("Click");

      if(currentTower == 0)
      {
        currentTower =  towerSprites.Length - 1;
      }
      else
      {
        currentTower -= 1;
      }
      SetTower(currentTower);


    }

}
