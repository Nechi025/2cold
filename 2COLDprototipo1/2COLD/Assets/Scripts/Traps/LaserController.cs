using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [Header("Trap Settings")]
    public LaserTrap laserTrap; // Referencia al sistema de láseres que controla
    public int health = 100; // Vida del controlador
    public GameObject explosionEffect; // Efecto visual cuando el controlador es destruido

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificamos si la bala toca el controlador
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();

            if (bullet != null)
            {
                TakeDamage(bullet.damage); // Hacer daño al controlador
                //Destroy(collision.gameObject); // Destruir la bala
            }
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        // Si la vida del controlador llega a 0, se destruye
        if (health <= 0)
        {
            gameObject.SetActive(false);
            DestroyController();
        }
    }

    void DestroyController()
    {
         gameObject.SetActive(false);
        
        // Desactivar los láseres permanentemente
        laserTrap.DestroyController();

        //// Crear un efecto visual de explosión al destruir el controlador
        //if (explosionEffect != null)
        //{
        //    Instantiate(explosionEffect, transform.position, Quaternion.identity);
        //}

        // Destruir el objeto del controlador
       
    }
}
