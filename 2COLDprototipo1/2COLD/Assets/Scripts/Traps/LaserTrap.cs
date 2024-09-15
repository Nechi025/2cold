using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{


    [Header("Laser Settings")]
    public GameObject[] lasers; // Los tres l�seres
    public float laserActiveTime = 2f; // Tiempo que los l�seres est�n activos
    public float laserDeactivateTime = 0.001f; // Tiempo que los l�seres se apagan
    public GameObject laserController; // Controlador que se debe destruir

    [Header("Player Settings")]
    public Transform player;
    public int damage = 20; // Da�o que hace el l�ser al jugador
    private bool controllerDestroyed = false; // Verifica si el controlador ha sido destruido

    private float laserTimer; // Temporizador para manejar la activaci�n/desactivaci�n
    private bool lasersActive = true; // Indica si los l�seres est�n activos

    void Start()
    {
        laserTimer = laserActiveTime; // Inicializar el temporizador
        ActivateLasers(); // Activa los l�seres al inicio
    }

    void Update()
    {
        // Verificar si el controlador fue destruido
        if (controllerDestroyed)
        {
            // Si el controlador es destruido, apaga permanentemente los l�seres
            DeactivateLasers();
            return;
        }

        // Si el poder de congelamiento est� activo, se frena la l�gica de activaci�n/desactivaci�n
        if (GlobalPause.IsPaused())
        {
            return;
        }

        // Manejamos la activaci�n/desactivaci�n c�clica de los l�seres
        laserTimer -= Time.deltaTime;

        if (lasersActive && laserTimer <= 0)
        {
            // Apagar los l�seres por un instante
            DeactivateLasers();
            laserTimer = laserDeactivateTime;
        }
        else if (!lasersActive && laserTimer <= 0)
        {
            // Reactivar los l�seres despu�s del tiempo desactivado
            ActivateLasers();
            laserTimer = laserActiveTime;
        }
    }

    void ActivateLasers()
    {
        lasersActive = true;
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(true); // Activar el l�ser
        }
    }

    void DeactivateLasers()
    {
        lasersActive = false;
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false); // Desactivar el l�ser
        }
    }

    public void OnPlayerHitLaser()
    {
        // L�gica para cuando el jugador toca los l�seres
        LifeS playerLife = player.GetComponent<LifeS>();
        playerLife.GetDamage(damage);
    }

    public void DestroyController()
    {
        // L�gica para cuando el controlador de l�seres es destruido
        controllerDestroyed = true;
        laserController.SetActive(false); // Desactiva el controlador visualmente
    }
}
