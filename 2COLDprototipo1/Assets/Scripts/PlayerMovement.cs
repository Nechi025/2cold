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
    public float idleTimeThreshold = 2f; // Tiempo en segundos antes de pausar el juego si el jugador está inactivo

    void Update()
    {
        // Si el jugador se mueve, actualizamos el tiempo de la última entrada de movimiento
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

        // Rotar el jugador hacia la posición del ratón
        RotatePlayer();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        // Si el juego está pausado, salir del método de actualización
        if (GlobalPause.IsPaused())
            return;

        // Obtener las entradas del teclado para el movimiento
        

        // Movemos al jugador según la entrada
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void RotatePlayer()
    {
        // Obtener la posición del ratón en el mundo
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Obtenemos la dirección hacia la posición del ratón
        Vector2 lookDir = mousePos - rb.position;

        // Calculamos el ángulo para que el jugador mire hacia el ratón
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        // Aplicamos la rotación al Rigidbody
        rb.rotation = angle;
    }
}