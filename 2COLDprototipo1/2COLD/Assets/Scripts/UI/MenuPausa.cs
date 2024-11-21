using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject EfectosPantalla;
    [SerializeField] private GameObject crosshair; // Referencia al crosshair

    private bool isPaused = false;
    public static bool isGamePaused = false; // Nuevo bool público y estático

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
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
        menuPausa.SetActive(true);
        EfectosPantalla.SetActive(false);
        //crosshair.SetActive(false); // Desactiva el crosshair
        isGamePaused = true; // Cambia el bool al pausar
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
        EfectosPantalla.SetActive(true);
        //crosshair.SetActive(true); // Activa el crosshair
        isGamePaused = false; // Cambia el bool al pausar
    }

    public void Cerrar()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        isGamePaused = false; // Cambia el bool al pausar
        StartCoroutine(GotoCurrentLevel());
    }

    IEnumerator GotoCurrentLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentSceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
