using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    private static MenuMusicManager instance;

    void Awake()
    {
        // Check if an instance already exists
        if (instance != null)
        {
            // Check if other music managers are present in the scene
            if (FindObjectOfType<MusicManager>() != null || FindObjectOfType<SecondMusicManager>() != null)
            {
                Destroy(gameObject); // Destroy this MusicManager if others are present
                return;
            }

            Destroy(gameObject); // Destroy duplicate
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist through scene changes
        }
    }
}
