using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : ManagedUpdateBehavior
{
    public static PlayerMovement Instance;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 180f; // Rotación en grados por segundo
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera cam;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashLength = 0.5f, dashCooldown = 2f;

    private float dashCounter;
    private float dashCoolCounter;
    private float activeMoveSpeed;

    [Header("Idle/Freeze Timer")]
    public float idleTimeThreshold = 2f;
    public float timer;
    public float timerReset;
    private bool isTimerRunning = false;
    public float timerResetSpeed = 1f;
    public bool isInNoTimerZone = false;

    [Header("Dash State")]
    public bool isDashing = false;

    [Header("Animations")]
    public Animator playerAnim;
    private string currentState;
    private string currentScreen;
    const string PlayIdle = "PlayIdle";
    const string PlaySlidingAnim = "Slide";
    const string BaseScreen = "BaseScreen";
    const string FreezingScreen = "FreezingScreen";
    const string Screen = "Screen";

    [Header("Joystick Settings")]
    [SerializeField] private float deadzone = 0.3f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool isIdle = Mathf.Abs(horizontal) < deadzone && Mathf.Abs(vertical) < deadzone;

        // FREEZE (idle)
        if (isIdle)
        {
            GlobalPause.isPaused = true;
            ChangeAnimationScreen(FreezingScreen, Screen);
            StartTimer();
            rb.velocity = Vector2.zero;
        }
        else
        {
            if (GlobalPause.isPaused)
            {
                GlobalPause.isPaused = false;
                ChangeAnimationScreen(BaseScreen, Screen);
            }
            ProgressivelyResetTimer();
        }

        HandleDash();
        UpdateTimer();

        if (GlobalPause.IsPaused())
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // --- ASTEROIDS MOVEMENT STYLE ---

        // Rotar con izquierda / derecha
        float rotationAmount = -horizontal * rotationSpeed * Time.deltaTime;
        rb.MoveRotation(rb.rotation + rotationAmount);

        // Mover hacia adelante o atrás según rotación
        Vector2 forward = Quaternion.Euler(0, 0, rb.rotation) * Vector2.up;
        rb.velocity = forward * vertical * activeMoveSpeed;

        // Animación (puedes ajustar esto si tenés animaciones de movimiento real)
        if (Mathf.Abs(vertical) > deadzone)
        {
            ChangeAnimationState(PlayIdle); // Reemplaza por "PlayRun" si tenés animación de correr
        }
        else
        {
            ChangeAnimationState(PlayIdle);
        }
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dashCoolCounter <= 0 && dashCounter <= 0)
        {
            activeMoveSpeed = dashSpeed;
            dashCounter = dashLength;
            SoundManager.Instance.PlaySound("Dash");
            ChangeAnimationState(PlaySlidingAnim);
            isDashing = true;
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

        if (dashCoolCounter > 0) dashCoolCounter -= Time.deltaTime;
    }

    void StartTimer()
    {
        if (!isTimerRunning) isTimerRunning = true;
    }

    void ProgressivelyResetTimer()
    {
        if (!isTimerRunning) return;
        timer = Mathf.Min(timer + timerResetSpeed * Time.deltaTime, timerReset);
    }

    void UpdateTimer()
    {
        if (!isTimerRunning || isInNoTimerZone) return;
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Debug.Log("¡Tiempo agotado! ¡El jugador pierde!");
            LifeS life = transform.GetComponent<LifeS>();
            life.GetDamage(100);
        }
    }
}
