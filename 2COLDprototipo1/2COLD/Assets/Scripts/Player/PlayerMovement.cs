using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance; // Singleton instance

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = 2f;

    private float dashCounter;
    private float dashCoolCounter;
    private float activeMoveSpeed;

    private Vector2 movement;
    private float lastMovementTime;
    public float idleTimeThreshold = 2f; // Tiempo en segundos antes de pausar el juego si el jugador está inactivo

    // Nuevo código para el temporizador
    public float timer = 3f; // Tiempo inicial del temporizador
    private bool isTimerRunning = false; // Bandera para controlar si el temporizador está corriendo

    // Nuevo código para el dash
    public bool isDashing = false; // Bandera para indicar si el dash está activo

    void Awake()
    {
        // Setup the singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {
        // Si el jugador se mueve, actualizamos el tiempo de la última entrada de movimiento
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            GlobalPause.isPaused = true;
            StartTimer(); // Inicia el temporizador cuando el jugador está inactivo
        }
        else
        {
            GlobalPause.isPaused = false;
            ResetTimer(); // Reinicia el temporizador cuando el jugador se mueve
        }

        // Rotar el jugador hacia la posición del ratón
        RotatePlayer();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        UpdateTimer();

        // Dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                isDashing = true; // El dash está activo
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
                isDashing = false; // El dash ya no está activo
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

        // Si el juego está pausado, salir del método de actualización
        if (GlobalPause.IsPaused())
            return;

        // Movemos al jugador según la entrada
        rb.MovePosition(rb.position + movement * activeMoveSpeed * Time.fixedDeltaTime);

        // Actualizar el temporizador
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

    // Iniciar el temporizador
    void StartTimer()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            timer = 3f; // Establecer el tiempo inicial del temporizador
        }
    }

    // Reiniciar el temporizador
    void ResetTimer()
    {
        isTimerRunning = false;
    }

    // Actualizar el temporizador
    void UpdateTimer()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime; // Reducir el temporizador
            if (timer <= 0f)
            {
                // Si el temporizador llega a cero, el jugador pierde
                Debug.Log("¡Tiempo agotado! ¡El jugador pierde!");
                LifeS life = transform.GetComponent<LifeS>();
                life.GetDamage(100);
                // Aquí puedes agregar el código para manejar la pérdida del jugador, como reiniciar el nivel o mostrar un mensaje de game over
            }
        }
    }
}
