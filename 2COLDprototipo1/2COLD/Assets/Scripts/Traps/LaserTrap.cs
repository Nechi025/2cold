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
                //Debug.Log("El juego está pausado. Los láseres no se activarán.");
                yield return null; 
            }
            else
            {
                
                ActivateLasers();
                //Debug.Log("Láseres ACTIVOS.");
                yield return new WaitForSeconds(laserActiveTime);

                
                DeactivateLasers();
                //Debug.Log("Láseres DESACTIVADOS.");
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
        //Debug.Log("Los láseres han sido activados.");
    }

    private void DeactivateLasers()
    {
        lasersActive = false;
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false); 
        }
        //Debug.Log("Los láseres han sido desactivados.");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
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
                    playerLife.GetDamage(laserDamage); 
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
        DeactivateLasers(); 
        StopAllCoroutines(); 
        //Debug.Log("Controlador destruido. Láseres desactivados permanentemente.");
    }
}
