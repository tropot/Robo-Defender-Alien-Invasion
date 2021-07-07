using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptForAnimatingEveryButtons : MonoBehaviour
{


    public void Disapier()
    {
      LeanTween.scale(gameObject,new Vector3(0,0,0), 0.5f).setOnComplete(DestroyMe);
    }
    private void DestroyMe()
    {
      Destroy(gameObject);
    }

}
