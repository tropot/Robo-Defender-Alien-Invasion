using UnityEngine;

public class MusicManager : MonoBehaviour
{


    private static MusicManager MusicManagerInstance;

    void Awake()
    {

      DontDestroyOnLoad(this);

      if(MusicManagerInstance == null)
      {
          MusicManagerInstance = this;
      }
      else
      {
        Destroy(gameObject);
      }
    }
}
