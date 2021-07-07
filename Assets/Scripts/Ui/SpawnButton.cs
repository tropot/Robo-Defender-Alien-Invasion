using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{

    public GameObject m;
    public GameObject a;
    public GameObject l;
    public GameObject r;
    public GameObject FOne;
    public GameObject FTwo;
    public GameObject placeHolder;

    public int grid;
    public int ord;
    GameObject p;


    public void sFOne()
    {
      p = Instantiate(FOne);
      MiniButton t = p.GetComponent<MiniButton>();

      t.idMini = ord;
      t.gridOnWitchSpawned = grid;
      p.transform.SetParent(placeHolder.transform);
    }

    public void sFTwo()
    {
      p = Instantiate(FTwo);
      MiniButton t = p.GetComponent<MiniButton>();

      t.idMini = ord;
      t.gridOnWitchSpawned = grid;
      p.transform.SetParent(placeHolder.transform);
    }

    public void sM()
    {
      p = Instantiate(m);
      MiniButton t = p.GetComponent<MiniButton>();

      t.idMini = ord;
      t.gridOnWitchSpawned = grid;
      p.transform.SetParent(placeHolder.transform);
    }

    public void sA()
    {
      p = Instantiate(a);
      MiniButton t = p.GetComponent<MiniButton>();

      t.idMini = ord;
      t.gridOnWitchSpawned = grid;
      p.transform.SetParent(placeHolder.transform);
    }

    public void sR()
    {
      p = Instantiate(r);
      MiniButton t = p.GetComponent<MiniButton>();

      t.idMini = ord;
      t.gridOnWitchSpawned = grid;
      p.transform.SetParent(placeHolder.transform);
    }

    public void sL()
    {
      p = Instantiate(l);
      MiniButton t = p.GetComponent<MiniButton>();

      t.idMini = ord;
      t.gridOnWitchSpawned = grid;
      p.transform.SetParent(placeHolder.transform);
    }




}
