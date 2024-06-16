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

    public void Pausa()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }


    public void Reanudar()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
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
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
