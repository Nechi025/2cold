using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMusicManager : MonoBehaviour
{
    private static SecondMusicManager instance;

    void Awake()
    {
        
        if (instance != null)
        {
            Destroy(gameObject);
            // Check if other music managers are present in the scene
            if (FindObjectOfType<MenuMusicManager>() != null || FindObjectOfType<MusicManager>() != null)
            {
                Destroy(gameObject); // Destroy this MusicManager if others are present
                return;
            }

             // Destroy duplicate
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist through scene changes
        }
    }
}
