using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{



    public List<int> order = new List<int>();
    public List<int> orderFOne = new List<int>();
    public List<int> orderFTwo = new List<int>();

    public List<int> actionOrder = new List<int>();

    public Transform pointOne;
    public Transform pointTwo;
    public LayerMask enemyLayers;
    public bool started = false;

    int isExecutingNeeded = 0;

    public int currentTab = 3;

    int i = 0;

    public int s = 0;

    SpawnButton sb;
    SpawnButton sbfOne;
    SpawnButton sbfTwo;
    public int nrOfEnemies;

    int orderSize;


    public float timeForTimer = 1;
    public float timeForEnemy = 1;

    MovementScript ms;
    MiniButton mb;
    GameObject[] enemies;
    GameObject[] mButtons;
    public Text cText;

    public int maxCommands = 5;



    float timeRemaining = 1;
    float timeRemainingForEnemy = 1;

    void Awake()
    {
      ms = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>();
      sb = GameObject.FindGameObjectWithTag("SpawnButton").GetComponent<SpawnButton>();





    }
    void Start()
    {
      order = PlayerPrefsExtra.GetList<int> ("order", new List<int>());
      orderFOne = PlayerPrefsExtra.GetList<int> ("orderFOne", new List<int>());
      orderFTwo = PlayerPrefsExtra.GetList<int> ("orderFTwo", new List<int>());
      setText(order.Count,maxCommands);
      matchGrid(currentTab,0);
      isExecutingNeeded = PlayerPrefs.GetInt("isExecutingNeeded");

      if(isExecutingNeeded > 0)
      {
        onStart();

      }

    }


    void Update()
    {
      checkIfGameOver();


      if(started)
      {
      if(actionOrder.Count > i)
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

          }

        }
      }
      else
      {
        started = false;
        i = 0;


      }
    }
    if(nrOfEnemies == 0)
    {
      order.Clear();
      orderFOne.Clear();
      orderFTwo.Clear();
      saveOrders();
      isExecutingNeeded = 0;
      PlayerPrefs.SetInt("isExecutingNeeded", isExecutingNeeded);
      Scene scene = SceneManager.GetActiveScene();
      SceneManager.LoadScene(sceneBuildIndex:scene.buildIndex+1);
    }
    if(started)
    {


        if(timeRemainingForEnemy > 0)
        {
          timeRemainingForEnemy -= Time.deltaTime;
        }
        else
        {
          enemies = GameObject.FindGameObjectsWithTag("Enemy");
          foreach(GameObject enemi in enemies)
          {
            enemi.GetComponent<EnemyBase>().matchAction();
          }
          timeRemainingForEnemy = timeForEnemy;
        }

    }





      if (CrossPlatformInputManager.GetButtonDown("Start"))
      {
        onStart();

        if(isExecutingNeeded > 0)
        {

          resetScene();
        }
        else
        {
          isExecutingNeeded += 1;
          PlayerPrefs.SetInt("isExecutingNeeded", isExecutingNeeded);
        }


      }
      if(CrossPlatformInputManager.GetButtonDown("Left"))
      {
        matchGrid(currentTab,3);
      }
      if(CrossPlatformInputManager.GetButtonDown("Right"))
      {
        matchGrid(currentTab,2);
      }
      if(CrossPlatformInputManager.GetButtonDown("Move"))
      {
        matchGrid(currentTab,1);
      }
      if(CrossPlatformInputManager.GetButtonDown("Attack"))
      {
        matchGrid(currentTab,4);
      }

      if(CrossPlatformInputManager.GetButtonDown("FOne"))
      {
        matchGrid(currentTab,5);
      }
      if(CrossPlatformInputManager.GetButtonDown("FTwo"))
      {
        matchGrid(currentTab,6);
      }
      if(CrossPlatformInputManager.GetButtonDown("Reset"))
      {
        isExecutingNeeded = 0;
        PlayerPrefs.SetInt("isExecutingNeeded", isExecutingNeeded);
        resetScene();
      }



    }
    void onStart()
    {

      actionOrder.Clear();
      matchActionList();
      timeRemaining = timeForTimer;
      started = true;
      i = 0;
      matchAction();
      enemies = GameObject.FindGameObjectsWithTag("Enemy");
      foreach(GameObject enemi in enemies)
      {
        enemi.GetComponent<EnemyBase>().matchAction();
      }
      matchGrid(currentTab,0);

    }

    void OnApplicationQuit()
    {
      order.Clear();
      orderFOne.Clear();
      orderFTwo.Clear();
      saveOrders();
    }
    public void resetScene()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void matchAction()
    {
      if(actionOrder.Count > i)
      {

        switch (actionOrder[i])
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
            ms.attack();
            break;
        }
        i += 1;
      }

    }
    public void listEdit(int number)
    {
      switch (currentTab)
      {
        case 1:
          //F1
          orderFOne.RemoveAt(number);
          break;
        case 2:
          //F2
          orderFTwo.RemoveAt(number);
          break;
        case 3:
          //Main
          order.RemoveAt(number);
          break;
      }


    }

    public void matchActionList()
    {

      s = 0;
      for(int p = 0;p < order.Count; p++)
      {
        switch (order[s])
        {
          case 1:
            //move
            actionOrder.Add(1);
            break;
          case 2:
            //right
            actionOrder.Add(2);
            break;
          case 3:
            //left
            actionOrder.Add(3);
            break;
          case 4:
            //attack
            actionOrder.Add(4);
            break;
          case 5:
            //F1
            for (int i = 0; i < orderFOne.Count; i++)
            {
                actionOrder.Add(orderFOne[i]);
            }
            break;
          case 6:
            //F2
            for (int i = 0; i < orderFTwo.Count; i++)
            {
                actionOrder.Add(orderFTwo[i]);
            }

            break;
        }
        s += 1;

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
            case 5:
              //F1
              sb.ord = s;
              sb.sFOne();
              break;
            case 6:
              //F2
              sb.ord = s;
              sb.sFTwo();
              break;
          }
          s += 1;
        }

    }

    }
    public void matchListFOne()
    {
      sbfOne = GameObject.FindGameObjectWithTag("SpawnButtonFOne").GetComponent<SpawnButton>();


      s = 0;
      for(int k = 0;k < orderFOne.Count;k++)
      {
        if(orderFOne.Count >= s)
        {
          sbfOne.ord = s;
          switch (orderFOne[s])
          {
            case 1:
              //move
              sbfOne.ord = s;
              sbfOne.sM();
              break;
            case 2:
              //right
              sbfOne.ord = s;
              sbfOne.sR();
              break;
            case 3:
              //left
              sbfOne.ord = s;
              sbfOne.sL();
              break;
            case 4:
              //attack
              sbfOne.ord = s;
              sbfOne.sA();
              break;
          }
          s += 1;
        }

    }

    }
    public void matchListFTwo()
    {


      sbfTwo = GameObject.FindGameObjectWithTag("SpawnButtonFTwo").GetComponent<SpawnButton>();

      s = 0;
      for(int k = 0;k < orderFTwo.Count;k++)
      {
        if(orderFTwo.Count >= s)
        {
          sbfTwo.ord = s;
          switch (orderFTwo[s])
          {
            case 1:
              //move
              sbfTwo.ord = s;
              sbfTwo.sM();
              break;
            case 2:
              //right
              sbfTwo.ord = s;
              sbfTwo.sR();
              break;
            case 3:
              //left
              sbfTwo.ord = s;
              sbfTwo.sL();
              break;
            case 4:
              //attack
              sbfTwo.ord = s;
              sbfTwo.sA();
              break;
          }
          s += 1;
        }

    }

    }
    public void matchGrid(int number,int action)
    {


      mButtons = GameObject.FindGameObjectsWithTag("MiniButton");

      foreach(GameObject obj in mButtons)
      {
        obj.GetComponent<MiniButton>().des();
      }
      switch (number)
      {
        case 1:
          //F1
          if(action > 0 & orderFOne.Count < maxCommands)
          {
            orderFOne.Add(action);
          }
          setText(orderFOne.Count,maxCommands);
          matchListFOne();
          break;
        case 2:
          //F2
          if(action > 0 & orderFTwo.Count < maxCommands)
          {
            orderFTwo.Add(action);
          }
          setText(orderFTwo.Count,maxCommands);
          matchListFTwo();
          break;
        case 3:
          //Main
          if(action > 0 & order.Count < maxCommands)
          {
            order.Add(action);
          }
          setText(order.Count,maxCommands);
          matchList();
          break;

      }
      saveOrders();
    }
    public void setText(int one,int two)
    {
      cText.text = string.Format("{0} / {1} commands",one,two);
    }
    public void checkIfGameOver()
    {
      Collider2D[] hitEnemys = Physics2D.OverlapAreaAll(pointOne.position,pointTwo.position, enemyLayers);

      foreach(Collider2D enemy in hitEnemys)
      {
        isExecutingNeeded = 0;
        PlayerPrefs.SetInt("isExecutingNeeded", isExecutingNeeded);
        resetScene();
      }
    }
    public void saveOrders()
    {
      PlayerPrefsExtra.SetList("order", order);
      PlayerPrefsExtra.SetList("orderFOne", orderFOne);
      PlayerPrefsExtra.SetList("orderFTwo", orderFTwo);
    }




}
