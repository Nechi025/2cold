using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : ManagedUpdateBehavior
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
    public float timer; // Tiempo inicial del temporizador
    public float timerReset; // Tiempo inicial del temporizador
    private bool isTimerRunning = false; // Bandera para controlar si el temporizador está corriendo
    public float timerResetSpeed = 1f; // Velocidad a la que el temporizador se reinicia progresivamente

    // Nuevo código para el dash
    public bool isDashing = false; // Bandera para indicar si el dash está activo

    public Animator playerAnim;
    private string currentState;
    private string currentState2;
    const string PlayIdle = "PlayIdle";
    const string PlaySlidingAnim = "Slide";
    const string BaseScreen = "BaseScreen";
    const string FreezingScreen = "FreezingScreen";

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

    protected override void Start()
    {
        base.Start();
        activeMoveSpeed = moveSpeed;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        playerAnim.Play(newState);
        currentState = newState;
    }

    void ChangeAnimationState2(string newState2)
    {
        playerAnim.Play(newState2);
        currentState2 = newState2;
    }

    public override void UpdateMe()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            GlobalPause.isPaused = true;
            StartTimer();
        }
        else
        {
            GlobalPause.isPaused = false;
            ProgressivelyResetTimer();
        }

        RotatePlayer();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Dash
        HandleDash();

        UpdateTimer();

        // Si el juego está pausado, salir del método de actualización
        if (GlobalPause.IsPaused())
            return;

        rb.MovePosition(rb.position + movement * activeMoveSpeed * Time.fixedDeltaTime);

        
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                SoundManager.Instance.PlaySound("Dash");
                ChangeAnimationState(PlaySlidingAnim);
                isDashing = true;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
                isDashing = false;
                ChangeAnimationState(PlayIdle);
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    void RotatePlayer()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void StartTimer()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            
        }
    }


    //ChangeAnimationState2(FreezingScreen);
    //ChangeAnimationState2(BaseScreen);

    void ProgressivelyResetTimer()
    {
        if (isTimerRunning)
        {
            
            timer += timerResetSpeed * Time.deltaTime;
            if (timer > timerReset)
            {
                timer = timerReset;
               
            }
        }
    }

    void UpdateTimer()
    {
        if (isTimerRunning)
        {           
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                Debug.Log("¡Tiempo agotado! ¡El jugador pierde!");
                LifeS life = transform.GetComponent<LifeS>();
                life.GetDamage(100);
            }
        }
    }


}
