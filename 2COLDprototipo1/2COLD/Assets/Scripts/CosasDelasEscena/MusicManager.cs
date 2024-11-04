using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;

    public AudioClip gameplay1Music;
    public AudioClip gameplay2Music;
    public AudioClip victoryMusic;
    public AudioClip defeatMusic;
    public AudioClip mainMenuMusic;


    void Awake()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this GameObject persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any new instances to keep only one
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to avoid memory leaks
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeMusicForScene(scene.name);
    }

    public void ChangeMusicForScene(string sceneName)
    {
        AudioClip newClip = null;

        switch (sceneName)
        {
            case "Menu Scene":
                newClip = mainMenuMusic;
                break;
            case "Victoria":
                newClip = victoryMusic;
                break;
            case "Derrota":
                newClip = defeatMusic;
                break;
            case "Level 6":
                newClip = gameplay2Music;
                break;
            default:
                newClip = gameplay1Music; // Use default gameplay music for other scenes
                break;
        }

        if (newClip != null && audioSource.clip != newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }
}