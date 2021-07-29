using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabButton : MonoBehaviour
{

    public TabGroup tabGroup;
    GameController gc;
    public Image background;

    // Start is called before the first frame update
    void Awake()
    {
      background = GetComponent<Image>();
      tabGroup.Subscribe(this);
    }

    public void pressed()
    {
      FindObjectOfType<AudioManager>().Play("Click");
      tabGroup.OnTabSelected(this);
      gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
      gc.matchGrid(gc.currentTab,0);

    }
}
