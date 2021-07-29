using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]int index;
    int levelsUnloked;
    Image image;
    bool locked = false;
    Text txt;
    void Start()
    {
      levelsUnloked = PlayerPrefs.GetInt("levelsUnloked");
      if(levelsUnloked == 0)
      {
        levelsUnloked = 1;
      }
      txt = GetComponentInChildren<Text>();
      txt.fontSize = 30;
      txt.text = string.Format("lvl {0}",index);
      image = GetComponent<Image>();
      if(index > levelsUnloked)
      {
        txt.text = string.Format(" ");
        image.color = new Color32(0,0,0,255);
        locked = true;
      }
    }

    public void loadLevel()
    {
      if(locked == false)
      {
        LeanTween.scale(gameObject,new Vector3(20,20,1), 0.1f).setOnComplete(ld);
        FindObjectOfType<AudioManager>().Play("Click");
      }

    }
    void ld()
    {
      SceneManager.LoadScene(sceneBuildIndex:index);
    }



}
