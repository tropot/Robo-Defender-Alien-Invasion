using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;





public class GameController : MonoBehaviour
{

    public List<Commands> order = new List<Commands>();
    public List<Commands> orderFOne = new List<Commands>();
    public List<Commands> orderFTwo = new List<Commands>();
    public List<Commands> currentOrder = new List<Commands>();
    public Stack<List<Commands>> arrayStack = new Stack<List<Commands>>();
    public Stack<int> indexStack = new Stack<int>();

    public Canvas mainCanvas;
    public Transform pointOne;
    public Transform pointTwo;
    public LayerMask enemyLayers;
    public bool started = false;

    int currentIndex = 0;




    int isExecutingNeeded = 0;

    public int currentTab = 3;



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
    public GameObject[] mButtons;
    public Text cText;

    public int maxCommands = 5;



    float timeRemaining = 1;
    float timeRemainingForEnemy = 1;

    void Awake()
    {
      ms = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>();
      sb = GameObject.FindGameObjectWithTag("SpawnButton").GetComponent<SpawnButton>();

      order = PlayerPrefsExtra.GetList<Commands> ("order", new List<Commands>());
      orderFOne = PlayerPrefsExtra.GetList<Commands> ("orderFOne", new List<Commands>());
      orderFTwo = PlayerPrefsExtra.GetList<Commands> ("orderFTwo", new List<Commands>());
      isExecutingNeeded = PlayerPrefs.GetInt("isExecutingNeeded");



    }
    void Start()
    {
      setText(order.Count,maxCommands);
      matchGrid(currentTab,Commands.nothing);

      enemies = GameObject.FindGameObjectsWithTag("Enemy");
      nrOfEnemies = enemies.Length;
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
    if(nrOfEnemies == 0)
    {

      isExecutingNeeded = 0;
      PlayerPrefs.SetInt("isExecutingNeeded", isExecutingNeeded);
      mainCanvas.GetComponent<NextLevelUi>().ActivateUi();

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
        matchGrid(currentTab,Commands.left);
      }
      if(CrossPlatformInputManager.GetButtonDown("Right"))
      {
        matchGrid(currentTab,Commands.right);
      }
      if(CrossPlatformInputManager.GetButtonDown("Move"))
      {
        matchGrid(currentTab,Commands.move);
      }
      if(CrossPlatformInputManager.GetButtonDown("Attack"))
      {
        matchGrid(currentTab,Commands.attack);
      }

      if(CrossPlatformInputManager.GetButtonDown("FOne"))
      {
        matchGrid(currentTab,Commands.fOne);
      }
      if(CrossPlatformInputManager.GetButtonDown("FTwo"))
      {
        matchGrid(currentTab,Commands.fTwo);
      }
      if(CrossPlatformInputManager.GetButtonDown("Reset"))
      {
        isExecutingNeeded = 0;
        PlayerPrefs.SetInt("isExecutingNeeded", isExecutingNeeded);
        resetScene();
      }
      if(CrossPlatformInputManager.GetButtonDown("Activate"))
      {
      //  showCurrentAction();
      }


    }
    void onStart()
    {

      currentOrder = order;

      timeRemaining = timeForTimer;
      started = true;
      currentIndex = 0;
      matchAction();

      enemies = GameObject.FindGameObjectsWithTag("Enemy");
      foreach(GameObject enemi in enemies)
      {
        enemi.GetComponent<EnemyBase>().matchAction();
      }
      matchGrid(currentTab,Commands.nothing);

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
      //Debug.Log("currentIndex : " + currentIndex);
      //Debug.Log("currentOrder.Count : " + currentOrder.Count);
      if(currentOrder.Count > currentIndex)
      {
        //Normal command
        //Debug.Log("inauntru la if");
        switchAction();
      }
      else if(arrayStack.Count > 0)
      {
        //We are inside a function and returning from it
        //Debug.Log("inauntru la else if");
        pop();

      }
      else
      {
        //When finished
        started = false;
        currentIndex = 0;
      }

}
/**
 * Pushing a list to array stack then replacing it with a recieving one, and pushing the current index and replacing it with 0
 */
    void pushReplace(List<Commands> list)
    {
      arrayStack.Push(currentOrder);
      currentOrder = list;
      indexStack.Push(currentIndex);
      currentIndex = 0;
    }

    void pop()
    {
      currentOrder = arrayStack.Pop();
      currentIndex = indexStack.Pop();
    }
/**
 * Matching current action
 */
    void switchAction()
    {
      switch (currentOrder[currentIndex++])
      {
        case Commands.move:
          ms.move();
          break;
        case Commands.right:
          ms.InputRight();
          break;
        case Commands.left:
          ms.InputLeft();
          break;
        case Commands.attack:
          ms.attack();
          break;
        case Commands.fOne:
           pushReplace(orderFOne);

           Debug.Log("arrayStack : " + arrayStack.Count);
           break;
        case Commands.fTwo:
          pushReplace(orderFTwo);
          break;
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
            case Commands.move:
              //move
              sb.ord = s;
              sb.sM();
              break;
            case Commands.right:
              //right
              sb.ord = s;
              sb.sR();
              break;
            case Commands.left:
              //left
              sb.ord = s;
              sb.sL();
              break;
            case Commands.attack:
              //attack
              sb.ord = s;
              sb.sA();
              break;
            case Commands.fOne:
              //F1
              sb.ord = s;
              sb.sFOne();
              break;
            case Commands.fTwo:
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
            case Commands.move:
              //move
              sbfOne.ord = s;
              sbfOne.sM();
              break;
            case Commands.right:
              //right
              sbfOne.ord = s;
              sbfOne.sR();
              break;
            case Commands.left:
              //left
              sbfOne.ord = s;
              sbfOne.sL();
              break;
            case Commands.attack:
              //attack
              sbfOne.ord = s;
              sbfOne.sA();
              break;
            case Commands.fOne:
              //F1
              sbfOne.ord = s;
              sbfOne.sFOne();
              break;
            case Commands.fTwo:
              //F2
              sb.ord = s;
              sb.sFTwo();
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
            case Commands.move:
              //move
              sbfTwo.ord = s;
              sbfTwo.sM();
              break;
            case Commands.right:
              //right
              sbfTwo.ord = s;
              sbfTwo.sR();
              break;
            case Commands.left:
              //left
              sbfTwo.ord = s;
              sbfTwo.sL();
              break;
            case Commands.attack:
              //attack
              sbfTwo.ord = s;
              sbfTwo.sA();
              break;
            case Commands.fOne:
              //F1
              sbfOne.ord = s;
              sbfOne.sFOne();
              break;
            case Commands.fTwo:
              //F2
              sb.ord = s;
              sb.sFTwo();
              break;
          }
          s += 1;
        }

    }

    }
    public void matchGrid(int number,Commands action)
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
