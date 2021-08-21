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
    int levelsUnloked = 1;
    int currentIndex = 0;



    public GameObject activate;
    public GameObject activateFOne;
    public GameObject activateFTwo;


    int isExecutingNeeded = 0;

    public int currentTab = 3;



    public int s = 0;
    int indexSaveForEvedentiation = 0;

    SpawnButton sb;
    SpawnButton sbfOne;
    SpawnButton sbfTwo;
    public int nrOfEnemies;

    int orderSize;


    public float timeForTimer = 1;

    MovementScript ms;
    MiniButton mb;
    GameObject[] enemies;
    public GameObject[] mButtons;
    public Text cText;

    public int maxCommands = 5;



    float timeRemaining = 1;

    void Awake()
    {
      ms = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>();
      sb = GameObject.FindGameObjectWithTag("SpawnButton").GetComponent<SpawnButton>();

      order = PlayerPrefsExtra.GetList<Commands> ("order", new List<Commands>());
      orderFOne = PlayerPrefsExtra.GetList<Commands> ("orderFOne", new List<Commands>());
      orderFTwo = PlayerPrefsExtra.GetList<Commands> ("orderFTwo", new List<Commands>());
      levelsUnloked = PlayerPrefs.GetInt("levelsUnloked");




    }
    void Start()
    {
      setText(order.Count,maxCommands);
      matchGrid(currentTab,Commands.nothing);

      enemies = GameObject.FindGameObjectsWithTag("Enemy");
      nrOfEnemies = enemies.Length;
      isExecutingNeeded = PlayerPrefs.GetInt("isExecutingNeeded");




      if(isExecutingNeeded == 1)
      {
        LeanTween.scale(gameObject,new Vector3(1,1,0), 0.1f).setOnComplete(onStart);
      }else{
        LeanTween.scale(gameObject,new Vector3(1,1,0), 0.1f);
      }

    }


    void Update()
    {
      checkIfGameOver();
      showCurrentAction();

      if(started)
      {

            if (timeRemaining > 0)
            {
              timeRemaining -= Time.deltaTime;
            }
            else
            {
              matchAction();

              enemies = GameObject.FindGameObjectsWithTag("Enemy");
              foreach(GameObject enemi in enemies)
              {
                enemi.GetComponent<EnemyBase>().matchAction();
              }

              timeRemaining = timeForTimer;

            }



    }
    if(nrOfEnemies == 0 & ms.dead == false)
    {

      isExecutingNeeded = 0;
      PlayerPrefs.SetInt("isExecutingNeeded", isExecutingNeeded);
      if(levelsUnloked < SceneManager.GetActiveScene().buildIndex)
      {
        levelsUnloked = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("levelsUnloked", levelsUnloked);
      }

      mainCanvas.GetComponent<NextLevelUi>().ActivateUi();


    }






      if (CrossPlatformInputManager.GetButtonDown("Start"))
      {
        FindObjectOfType<AudioManager>().Play("Click");

        if(isExecutingNeeded == 1)
        {
          resetScene();
        }


        onStart();
        PlayerPrefs.SetInt("isExecutingNeeded", isExecutingNeeded);

        isExecutingNeeded = 1;







      }

      if(CrossPlatformInputManager.GetButtonDown("Left"))
      {
        FindObjectOfType<AudioManager>().Play("Click");

        matchGrid(currentTab,Commands.left);
      }
      if(CrossPlatformInputManager.GetButtonDown("Right"))
      {
        FindObjectOfType<AudioManager>().Play("Click");

        matchGrid(currentTab,Commands.right);
      }
      if(CrossPlatformInputManager.GetButtonDown("Move"))
      {
        FindObjectOfType<AudioManager>().Play("Click");

        matchGrid(currentTab,Commands.move);
      }
      if(CrossPlatformInputManager.GetButtonDown("Attack"))
      {
        FindObjectOfType<AudioManager>().Play("Click");

        matchGrid(currentTab,Commands.attack);
      }

      if(CrossPlatformInputManager.GetButtonDown("FOne"))
      {
        FindObjectOfType<AudioManager>().Play("Click");

        matchGrid(currentTab,Commands.fOne);
      }
      if(CrossPlatformInputManager.GetButtonDown("FTwo"))
      {
        FindObjectOfType<AudioManager>().Play("Click");

        matchGrid(currentTab,Commands.fTwo);
      }
      if(CrossPlatformInputManager.GetButtonDown("Reset"))
      {
        FindObjectOfType<AudioManager>().Play("Click");

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
    void showCurrentAction()
    {
      mButtons = GameObject.FindGameObjectsWithTag("MiniButton");
      foreach(GameObject button in mButtons)
      {
        switch (currentTab)
        {
          case 1:
            //F1
            button.GetComponent<MiniButton>().currentAction(currentIndex - 1,currentTab);
            break;
          case 2:
            //F2
            button.GetComponent<MiniButton>().currentAction(currentIndex - 1,currentTab);
            break;
          case 3:
            //Main
            if(indexSaveForEvedentiation > 0)
            {
              button.GetComponent<MiniButton>().currentAction(indexSaveForEvedentiation - 1,currentTab);
            }
            else{
              button.GetComponent<MiniButton>().currentAction(currentIndex -1 ,currentTab);

            }
            break;
        }
      }
    }


    void OnApplicationQuit()
    {
      order.Clear();
      orderFOne.Clear();
      orderFTwo.Clear();
      isExecutingNeeded = 0;
      PlayerPrefs.SetInt("isExecutingNeeded", isExecutingNeeded);
      saveOrders();
    }
    public void resetScene()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void matchAction()
    {
      if(ms.dead == true)
      {
        started = false;
        return;
      }
      showCurrentAction();
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
        activate.GetComponent<ActivateButton>().activatePanel();
        activate.GetComponent<TabButton>().pressed();

      }
      else
      {
        //When finished
        started = false;
        //currentIndex = 0;
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
      indexSaveForEvedentiation = currentIndex;
      currentIndex = 0;
    }

    void pop()
    {
      indexSaveForEvedentiation = 0;
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
           activateFOne.GetComponent<ActivateButton>().activatePanel();
           activateFOne.GetComponent<TabButton>().pressed();

           break;
        case Commands.fTwo:
          pushReplace(orderFTwo);
          activateFTwo.GetComponent<ActivateButton>().activatePanel();
          activateFTwo.GetComponent<TabButton>().pressed();

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
              sbfOne.ord = s;
              sbfOne.sFTwo();
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
              sbfTwo.ord = s;
              sbfTwo.sFOne();
              break;
            case Commands.fTwo:
              //F2
              sbfTwo.ord = s;
              sbfTwo.sFTwo();
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
          if(action > 0 & orderFOne.Count < maxCommands & started != true)
          {
            orderFOne.Add(action);
          }
          setText(orderFOne.Count,maxCommands);
          matchListFOne();
          break;
        case 2:
          //F2
          if(action > 0 & orderFTwo.Count < maxCommands & started != true)
          {
            orderFTwo.Add(action);
          }
          setText(orderFTwo.Count,maxCommands);
          matchListFTwo();
          break;
        case 3:
          //Main
          if(action > 0 & order.Count < maxCommands & started != true)
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
    public void clearLists()
    {
      if(started != true)
      {
        order.Clear();
        orderFOne.Clear();
        orderFTwo.Clear();
        matchGrid(currentTab,0);
      }

    }




}
