using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [Header("Trap Settings")]
    public LaserTrap laserTrap; 
    public int health = 100; 
    public GameObject explosionEffect; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();

            if (bullet != null)
            {
                TakeDamage(bullet.damage); 
                 
            }
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        
        if (health <= 0)
        {
            gameObject.SetActive(false);
            DestroyController();
        }
    }

    void DestroyController()
    {
         gameObject.SetActive(false);
        
        
        laserTrap.DestroyController();

        
        
       
    }
}
