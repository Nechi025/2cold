using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{

    public GameObject currentPanel;
    [SerializeField] Button elBoton;
    [SerializeField] Button Option;
    [SerializeField] Button back;

    private void Start()
    {
        elBoton.interactable = true;
        

    }
    



    public void OnPlay()
    {
        LoadingManager.Instance.LoadScene(1, 2);
    }

    public void Options()
    {
        LoadingManager.Instance.LoadScene(1, 6);
    }

    public void Back()
    {
        LoadingManager.Instance.LoadScene(6, 1);
    }

    public void Restart()
    {
        LoadingManager.Instance.LoadScene(5, 1);
    }

    public void Reset()
    {
        
        SceneManager.LoadScene(0);
    }

    public void Next()
    {
        LoadingManager.Instance.LoadScene(5, 3);
    }

    public void Cerrar()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        // Obtiene el nombre de la escena actual
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Reinicia la escena actual
        SceneManager.LoadScene(currentSceneName);
    }

}
