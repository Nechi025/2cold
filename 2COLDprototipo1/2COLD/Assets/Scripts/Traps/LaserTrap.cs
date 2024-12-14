using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    [Header("Laser Settings")]
    public GameObject[] lasers; 
    public float laserActiveTime = 2f; 
    public float laserInactiveTime = 0.001f; 
    public int laserDamage = 20; 
    public LayerMask playerLayer; 

    private bool lasersActive = true; 
    private bool controllerDestroyed = false; 

    private void Start()
    {
        
        StartCoroutine(LaserRoutine());
    }

    private IEnumerator LaserRoutine()
    {
        while (!controllerDestroyed)
        {
            
            if (GlobalPause.IsPaused())
            {
                
                yield return null; 
            }
            else
            {
                
                ActivateLasers();
                
                yield return new WaitForSeconds(laserActiveTime);

                
                DeactivateLasers();
                
                yield return new WaitForSeconds(laserInactiveTime); 
            }
        }
    }

    private void ActivateLasers()
    {
        lasersActive = true;
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(true); 
        }
        
    }

    private void DeactivateLasers()
    {
        lasersActive = false;
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false); 
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (lasersActive)
        {
            

            if (collision.gameObject.CompareTag("Player"))
            {
                

                LifeS playerLife = collision.GetComponent<LifeS>();
                if (playerLife != null)
                {
                    
                    playerLife.GetDamage(laserDamage); 
                }
                
            }
            
        }
       
    }

    public void DestroyController()
    {
        controllerDestroyed = true;
        DeactivateLasers(); 
        StopAllCoroutines(); 
        
    }
}
