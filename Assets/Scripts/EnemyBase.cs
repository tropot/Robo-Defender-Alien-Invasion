using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public EnemiMovementScript ms;
    int i = 0;
    public List<int> order = new List<int>();
    public bool isBig = false;
    GameController gc;
    private bool Died = false;
    string[] soundNames = new string[]{"EnemiSoundOne","EnemiSoundTwo","EnemiSoundThre"};
    string soundName;
    int randomNr;

    void Start()
    {
      gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }


    void Update()
    {


      if(isBig == true)
      {
          ms.attack();
      }
      else
      {
          ms.enemiBoom();
      }



    }

    public void TakeDamage()
    {
        if(Died == false)
        {

          Died = true;
          ms.moveSpeed = 0;
          ms.animateDeath();
          FindObjectOfType<AudioManager>().Play("EnemiDeath");
        }

    }

    public void Die()
    {
      gc.nrOfEnemies -= 1;
      Destroy(gameObject);
    }

    public void matchAction()
    {
      if(order.Count > i)
      {
        randomNr = Random.Range(0, soundNames.Length-1);
        soundName = soundNames[randomNr];
        FindObjectOfType<AudioManager>().Play(soundName);
        switch (order[i])
        {
          case 1:
            //move
            ms.move();
            break;
          case 2:
            //right
            ms.InputRight();
            break;
          case 3:
            //left
            ms.InputLeft();
            break;
          case 4:
            //attack
            //ms.attack();
            break;
        }
        i += 1;
      }
    }


}
