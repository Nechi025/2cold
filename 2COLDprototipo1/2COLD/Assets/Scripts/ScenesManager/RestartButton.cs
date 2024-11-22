using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void RestartLevel()
    {
        
        string lastLevel = PlayerPrefs.GetString("LastLevel");

        
        SceneManager.LoadScene(lastLevel);
    }
}
