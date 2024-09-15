using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    [Header("Laser Settings")]
    public GameObject[] lasers; // Los l�seres que estar�n activos y desactivados
    public float laserActiveTime = 2f; // Tiempo que los l�seres estar�n activos
    public float laserInactiveTime = 0.001f; // Tiempo que los l�seres estar�n apagados (ventana de oportunidad)
    public int laserDamage = 20; // Da�o que hacen los l�seres al jugador
    public LayerMask playerLayer; // La capa del jugador

    private bool lasersActive = true; // Estado de los l�seres (activos o inactivos)
    private bool controllerDestroyed = false; // Estado del controlador (si fue destruido o no)

    private void Start()
    {
        //Debug.Log("LaserTrap iniciado.");
        StartCoroutine(LaserRoutine());
    }

    private IEnumerator LaserRoutine()
    {
        while (!controllerDestroyed)
        {
            // L�gica para activar/desactivar los l�seres seg�n el estado del GlobalPause
            if (GlobalPause.IsPaused())
            {
                //Debug.Log("El juego est� pausado. Los l�seres no se activar�n.");
                yield return null; // Pausa la l�gica cuando el juego est� pausado
            }
            else
            {
                // Activar los l�seres
                ActivateLasers();
                //Debug.Log("L�seres ACTIVOS.");
                yield return new WaitForSeconds(laserActiveTime); // Espera el tiempo en el que los l�seres est�n activos

                // Desactivar los l�seres por un breve tiempo
                DeactivateLasers();
                //Debug.Log("L�seres DESACTIVADOS.");
                yield return new WaitForSeconds(laserInactiveTime); // Ventana de oportunidad
            }
        }
    }

    private void ActivateLasers()
    {
        lasersActive = true;
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(true); // Activa los l�seres
        }
        //Debug.Log("Los l�seres han sido activados.");
    }

    private void DeactivateLasers()
    {
        lasersActive = false;
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false); // Desactiva los l�seres
        }
        //Debug.Log("Los l�seres han sido desactivados.");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Verificar si el l�ser est� activo y el objeto es el jugador
        if (lasersActive)
        {
            //Debug.Log("L�seres activos: true");

            if (collision.gameObject.CompareTag("Player"))
            {
                /*Debug.Log("Jugador detectado en el �rea del l�ser.")*/;

                LifeS playerLife = collision.GetComponent<LifeS>();
                if (playerLife != null)
                {
                    //Debug.Log("Aplicando da�o al jugador.");
                    playerLife.GetDamage(laserDamage); // Aplica da�o al jugador
                }
                else
                {
                    //Debug.LogWarning("No se encontr� el componente LifeS en el jugador.");
                }
            }
            else
            {
                //Debug.Log("El objeto detectado no es el jugador.");
            }
        }
        else
        {
            //Debug.Log("L�seres activos: false");
        }
    }

    public void DestroyController()
    {
        controllerDestroyed = true;
        DeactivateLasers(); // Desactiva los l�seres permanentemente
        StopAllCoroutines(); // Detiene la rutina de activaci�n/desactivaci�n
        //Debug.Log("Controlador destruido. L�seres desactivados permanentemente.");
    }
}
