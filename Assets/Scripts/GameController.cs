using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameController : MonoBehaviour
{
    public List<int> order = new List<int>();

    public bool started = false;

    int i = 0;

    public int s = 0;

    SpawnButton sb;

    public float timeForTimer = 1;

    MovementScript ms;
    MiniButton mb;



    float timeRemaining;


    void Update()
    {
      if(order.Count > i)
      {
        if (started)
        {
          if (timeRemaining > 0)
          {
            timeRemaining -= Time.deltaTime;
          }
          else
          {
            matchAction();
            timeRemaining = timeForTimer;

            //started = false;
          }

        }
      }
      else
      {
        started = false;
        i = 0;
        order.Clear();

      }





      if (CrossPlatformInputManager.GetButtonDown("Start"))
      {
        timeRemaining = timeForTimer;
        started = true;
        i = 0;
      }
      if(CrossPlatformInputManager.GetButtonDown("Left"))
      {
        order.Add(3);
        matchList();
      }
      if(CrossPlatformInputManager.GetButtonDown("Right"))
      {
        order.Add(2);
        matchList();
      }
      if(CrossPlatformInputManager.GetButtonDown("Move"))
      {
        order.Add(1);
        matchList();
      }
      if(CrossPlatformInputManager.GetButtonDown("Attack"))
      {
        order.Add(4);
        matchList();
      }
      if(CrossPlatformInputManager.GetButtonDown("ds"))
      {
        if(order.Count >= 0)
        {
            mb = GameObject.FindGameObjectWithTag("MiniButton").GetComponent<MiniButton>();
            mb.des();
        }
        matchList();
      }


    }

    void matchAction()
    {
      if(order.Count > i)
      {
        ms = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>();
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
    public void listEdit(int number)
    {
      int temp = number;
      if(temp == 0)
      {
        order.Clear();
      }
      else
      {
        order.Remove(temp);
      }

    }

    public void matchList()
    {
      sb = GameObject.FindGameObjectWithTag("SpawnButton").GetComponent<SpawnButton>();


      s = 0;
      for(int k = 0;k < order.Count;k++)
      {
        if(order.Count >= s)
        {
          sb.ord = s;
          switch (order[s])
          {
            case 1:
              //move
              sb.ord = s;
              sb.sM();
              break;
            case 2:
              //right
              sb.ord = s;
              sb.sR();
              break;
            case 3:
              //left
              sb.ord = s;
              sb.sL();
              break;
            case 4:
              //attack
              sb.ord = s;
              sb.sA();
              break;
          }
          s += 1;
        }

    }

    }


}
