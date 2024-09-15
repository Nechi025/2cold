using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    [Header("Laser Settings")]
    public GameObject[] lasers; // Los láseres que estarán activos y desactivados
    public float laserActiveTime = 2f; // Tiempo que los láseres estarán activos
    public float laserInactiveTime = 0.001f; // Tiempo que los láseres estarán apagados (ventana de oportunidad)
    public int laserDamage = 20; // Daño que hacen los láseres al jugador
    public LayerMask playerLayer; // La capa del jugador

    private bool lasersActive = true; // Estado de los láseres (activos o inactivos)
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
            // Lógica para activar/desactivar los láseres según el estado del GlobalPause
            if (GlobalPause.IsPaused())
            {
                //Debug.Log("El juego está pausado. Los láseres no se activarán.");
                yield return null; // Pausa la lógica cuando el juego está pausado
            }
            else
            {
                // Activar los láseres
                ActivateLasers();
                //Debug.Log("Láseres ACTIVOS.");
                yield return new WaitForSeconds(laserActiveTime); // Espera el tiempo en el que los láseres están activos

                // Desactivar los láseres por un breve tiempo
                DeactivateLasers();
                //Debug.Log("Láseres DESACTIVADOS.");
                yield return new WaitForSeconds(laserInactiveTime); // Ventana de oportunidad
            }
        }
    }

    private void ActivateLasers()
    {
        lasersActive = true;
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(true); // Activa los láseres
        }
        //Debug.Log("Los láseres han sido activados.");
    }

    private void DeactivateLasers()
    {
        lasersActive = false;
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false); // Desactiva los láseres
        }
        //Debug.Log("Los láseres han sido desactivados.");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Verificar si el láser está activo y el objeto es el jugador
        if (lasersActive)
        {
            //Debug.Log("Láseres activos: true");

            if (collision.gameObject.CompareTag("Player"))
            {
                /*Debug.Log("Jugador detectado en el área del láser.")*/;

                LifeS playerLife = collision.GetComponent<LifeS>();
                if (playerLife != null)
                {
                    //Debug.Log("Aplicando daño al jugador.");
                    playerLife.GetDamage(laserDamage); // Aplica daño al jugador
                }
                else
                {
                    //Debug.LogWarning("No se encontró el componente LifeS en el jugador.");
                }
            }
            else
            {
                //Debug.Log("El objeto detectado no es el jugador.");
            }
        }
        else
        {
            //Debug.Log("Láseres activos: false");
        }
    }

    public void DestroyController()
    {
        controllerDestroyed = true;
        DeactivateLasers(); // Desactiva los láseres permanentemente
        StopAllCoroutines(); // Detiene la rutina de activación/desactivación
        //Debug.Log("Controlador destruido. Láseres desactivados permanentemente.");
    }
}
