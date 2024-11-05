using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    public AudioSource audioSource;
    public string SecondHalf;
    public string Defeat;
    public string Victory;
    public string MainMenu;
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
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
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

        if (sceneName == MainMenu)
        {
            newClip = mainMenuMusic;
        }
        else if (sceneName == Victory)
        {
            newClip = victoryMusic;
        }
        else if (sceneName == Defeat)
        {
            newClip = defeatMusic;
        }
        else if (sceneName == SecondHalf)
        {
            newClip = gameplay2Music;
        }
        else
        {
            newClip = gameplay1Music; // Default gameplay music for other scenes
        }

        if (newClip != null && audioSource.clip != newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }


        if (newClip != null && audioSource.clip != newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }
}
