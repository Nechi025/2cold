using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public string LevelTutorial;
    public string Level2;
    public string Level3;
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        isPaused = true;
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        isPaused = false;
    }

    public void Cerrar()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        StartCoroutine(GotoLevel(Level2));
    }

    public void Restart1()
    {
        Time.timeScale = 1f;
        StartCoroutine(GotoLevel(LevelTutorial));
    }

    public void Restart3()
    {
        Time.timeScale = 1f;
        StartCoroutine(GotoLevel(Level3));
    }

    IEnumerator GotoLevel(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
