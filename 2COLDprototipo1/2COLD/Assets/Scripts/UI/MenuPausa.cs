using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    //[SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject EfectosPantalla;

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
        //botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        EfectosPantalla.SetActive(false);
        isPaused = true;
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        //botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        EfectosPantalla.SetActive(true);
        isPaused = false;
    }

    public void Cerrar()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        StartCoroutine(GotoCurrentLevel());
    }

    IEnumerator GotoCurrentLevel()
    {
        // Obtiene el nombre de la escena actual
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Carga la escena actual nuevamente
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentSceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
