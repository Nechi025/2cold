using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;

    private Vector2 movement;
    private float lastMovementTime;
    public float idleTimeThreshold = 2f; // Tiempo en segundos antes de pausar el juego si el jugador est� inactivo

    void Update()
    {
        // Si el jugador se mueve, actualizamos el tiempo de la �ltima entrada de movimiento
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            GlobalPause.isPaused = true;
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == -1)
            {
                GlobalPause.isPaused = false;
            }
        }

        // Rotar el jugador hacia la posici�n del rat�n
        RotatePlayer();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        // Si el juego est� pausado, salir del m�todo de actualizaci�n
        if (GlobalPause.IsPaused())
            return;

        // Obtener las entradas del teclado para el movimiento
        

        // Movemos al jugador seg�n la entrada
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void RotatePlayer()
    {
        // Obtener la posici�n del rat�n en el mundo
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Obtenemos la direcci�n hacia la posici�n del rat�n
        Vector2 lookDir = mousePos - rb.position;

        // Calculamos el �ngulo para que el jugador mire hacia el rat�n
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        // Aplicamos la rotaci�n al Rigidbody
        rb.rotation = angle;
    }
}