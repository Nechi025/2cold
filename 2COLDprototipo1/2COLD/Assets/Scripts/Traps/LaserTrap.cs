using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{


    [Header("Laser Settings")]
    public GameObject[] lasers; // Los tres láseres
    public float laserActiveTime = 2f; // Tiempo que los láseres están activos
    public float laserDeactivateTime = 0.001f; // Tiempo que los láseres se apagan
    public GameObject laserController; // Controlador que se debe destruir

    [Header("Player Settings")]
    public Transform player;
    public int damage = 20; // Daño que hace el láser al jugador
    private bool controllerDestroyed = false; // Verifica si el controlador ha sido destruido

    private float laserTimer; // Temporizador para manejar la activación/desactivación
    private bool lasersActive = true; // Indica si los láseres están activos

    void Start()
    {
        laserTimer = laserActiveTime; // Inicializar el temporizador
        ActivateLasers(); // Activa los láseres al inicio
    }

    void Update()
    {
        // Verificar si el controlador fue destruido
        if (controllerDestroyed)
        {
            // Si el controlador es destruido, apaga permanentemente los láseres
            DeactivateLasers();
            return;
        }

        // Si el poder de congelamiento está activo, se frena la lógica de activación/desactivación
        if (GlobalPause.IsPaused())
        {
            return;
        }

        // Manejamos la activación/desactivación cíclica de los láseres
        laserTimer -= Time.deltaTime;

        if (lasersActive && laserTimer <= 0)
        {
            // Apagar los láseres por un instante
            DeactivateLasers();
            laserTimer = laserDeactivateTime;
        }
        else if (!lasersActive && laserTimer <= 0)
        {
            // Reactivar los láseres después del tiempo desactivado
            ActivateLasers();
            laserTimer = laserActiveTime;
        }
    }

    void ActivateLasers()
    {
        lasersActive = true;
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(true); // Activar el láser
        }
    }

    void DeactivateLasers()
    {
        lasersActive = false;
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false); // Desactivar el láser
        }
    }

    public void OnPlayerHitLaser()
    {
        // Lógica para cuando el jugador toca los láseres
        LifeS playerLife = player.GetComponent<LifeS>();
        playerLife.GetDamage(damage);
    }

    public void DestroyController()
    {
        // Lógica para cuando el controlador de láseres es destruido
        controllerDestroyed = true;
        laserController.SetActive(false); // Desactiva el controlador visualmente
    }
}
