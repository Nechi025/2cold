using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargaEscenas : MonoBehaviour
{
    
    public string sceneLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            SceneManager.LoadScene(sceneLoad);
        }
    }
}