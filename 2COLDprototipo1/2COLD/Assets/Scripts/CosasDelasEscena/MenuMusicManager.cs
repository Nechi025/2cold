using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    private static MenuMusicManager instance;

    void Awake()
    {
        
        if (instance != null)
        {
            
            if (FindObjectOfType<MusicManager>() != null || FindObjectOfType<SecondMusicManager>() != null)
            {
                Destroy(gameObject);
                return;
            }

            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
