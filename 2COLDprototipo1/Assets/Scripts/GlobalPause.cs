using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPause : MonoBehaviour
{
    public static bool isPaused = false;

    public static bool IsPaused()
    {
        return isPaused;
    }

    private void Update()
    {
        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }
    public static void TogglePause()
    {
        isPaused = !isPaused;

        // Pausar o reanudar el juego según el estado del pausado
        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }

        // Notificar a otros componentes sobre el cambio de estado de pausa si es necesario
    }

    public static void PauseGame()
    {
        // Poner en pausa todos los elementos que queremos detener

        // Por ejemplo, aquí puedes ajustar la velocidad de movimiento de otros objetos que necesitas detener
        GameObject[] objectsToPause = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in objectsToPause)
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public static void ResumeGame()
    {
        // Reanudar todos los elementos que fueron detenidos durante la pausa

        // Por ejemplo, aquí puedes restaurar la velocidad de movimiento de los objetos que detuviste
        GameObject[] objectsToResume = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in objectsToResume)
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.one;
        }
    }
}
