using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public EnemiMovementScript ms;
    int i = 0;
    public List<Commands> order = new List<Commands>();
    public bool isBig = false;
    GameController gc;
    private bool Died = false;
    string[] soundNames = new string[]{"EnemiSoundOne","EnemiSoundTwo","EnemiSoundThre"};
    string soundName;
    int randomNr;
    float timeRemaining = 0.55f;

    void Start()
    {
      gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }


    void Update()
    {


      if(Died)
      {
        if (timeRemaining > 0)
        {
          timeRemaining -= Time.deltaTime;
        }
        else
        {
          Die();
        }
      }


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
          case Commands.move:
            //move
            ms.move();
            break;
          case Commands.right:
            //right
            ms.InputRight();
            break;
          case Commands.left:
            //left
            ms.InputLeft();
            break;
        }
        i += 1;
      }
    }


}
