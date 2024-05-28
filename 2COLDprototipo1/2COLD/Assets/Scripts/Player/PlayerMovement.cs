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
    public float idleTimeThreshold = 2f; // Tiempo en segundos antes de pausar el juego si el jugador est� inactivo

    // Nuevo c�digo para el temporizador
    public float timer = 3f; // Tiempo inicial del temporizador
    private bool isTimerRunning = false; // Bandera para controlar si el temporizador est� corriendo

    // Nuevo c�digo para el dash
    public bool isDashing = false; // Bandera para indicar si el dash est� activo

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
        // Si el jugador se mueve, actualizamos el tiempo de la �ltima entrada de movimiento
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            GlobalPause.isPaused = true;
            StartTimer(); // Inicia el temporizador cuando el jugador est� inactivo
        }
        else
        {
            GlobalPause.isPaused = false;
            ResetTimer(); // Reinicia el temporizador cuando el jugador se mueve
        }

        // Rotar el jugador hacia la posici�n del rat�n
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
                isDashing = true; // El dash est� activo
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
                isDashing = false; // El dash ya no est� activo
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

        // Si el juego est� pausado, salir del m�todo de actualizaci�n
        if (GlobalPause.IsPaused())
            return;

        // Movemos al jugador seg�n la entrada
        rb.MovePosition(rb.position + movement * activeMoveSpeed * Time.fixedDeltaTime);

        // Actualizar el temporizador
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
                Debug.Log("�Tiempo agotado! �El jugador pierde!");
                LifeS life = transform.GetComponent<LifeS>();
                life.GetDamage(100);
                // Aqu� puedes agregar el c�digo para manejar la p�rdida del jugador, como reiniciar el nivel o mostrar un mensaje de game over
            }
        }
    }
}
