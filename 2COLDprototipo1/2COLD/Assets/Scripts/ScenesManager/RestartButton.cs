using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void RestartLevel()
    {
        // Obtiene el nombre del último nivel jugado
        string lastLevel = PlayerPrefs.GetString("LastLevel");

        // Carga el nivel almacenado
        SceneManager.LoadScene(lastLevel);
    }
}
