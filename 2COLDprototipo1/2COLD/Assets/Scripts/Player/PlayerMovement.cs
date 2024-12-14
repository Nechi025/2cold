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
    public float idleTimeThreshold = 2f; 

    
    public float timer; 
    public float timerReset; 
    private bool isTimerRunning = false; 
    public float timerResetSpeed = 1f; 
    public bool isInNoTimerZone = false; 


    
    public bool isDashing = false; 

    public Animator playerAnim;
    private string currentState;
    private string currentScreen;
    const string PlayIdle = "PlayIdle";
    const string PlaySlidingAnim = "Slide";
    const string BaseScreen = "BaseScreen";
    const string FreezingScreen = "FreezingScreen";
    const string Screen = "Screen";

    void Awake()
    {
        
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

    void ChangeAnimationScreen(string newScreen, string layerName)
    {
        int layerIndex = playerAnim.GetLayerIndex(layerName);
        playerAnim.Play(newScreen, layerIndex);
        currentScreen = newScreen;
    }


    public override void UpdateMe()
    {
        // Detecta si el jugador está inactivo
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            GlobalPause.isPaused = true;
            ChangeAnimationScreen(FreezingScreen, Screen);
            StartTimer();
            rb.velocity = Vector2.zero; // Detiene cualquier movimiento residual
        }
        else
        {
            GlobalPause.isPaused = false;
            ChangeAnimationScreen(BaseScreen, Screen);
            ProgressivelyResetTimer();
        }

        RotatePlayer();

        // Movimiento
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Dash
        HandleDash();

        UpdateTimer();

        
        if (GlobalPause.IsPaused())
        {
            rb.velocity = Vector2.zero; // Detener el movimiento residual al pausar
            return;
        }

        
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

                
                rb.velocity = Vector2.zero;
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
            
            if (!isInNoTimerZone)
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


}
