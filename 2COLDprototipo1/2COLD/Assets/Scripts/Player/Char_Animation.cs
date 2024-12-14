using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Animation : MonoBehaviour
{
    //este script es para las animaciones del player
    [Header("Animacion")]
    public Animator playerAnim;
    private float movimientoHorizontal;
    private float movimientoVertical;

    [SerializeField] private ParticleSystem polvoDash;
    [SerializeField] private ParticleSystemRenderer polvoRender;
    


    [SerializeField] public bool disparando;

    private string currentState;
    private string currentStateL;

    const string Legs = "Legs";
    const string PlayIdleState = "PlayWeaponRifle";
    const string PlayShooting = "PlayShootingRifle";
    const string PlayIdle = "PlayIdle";
    const string PlayWalkAnim = "PlayMoving";
    

    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;

    //atributos de animación de dash
    [SerializeField] private TrailRenderer trail;
    private float dashCounter;
    private float dashCoolCounter;
    public float dashLength = .5f, dashCooldown = 2f;
    public bool AnimationRapido = false;
    private bool isDashing = false;

    //atributos de animación de tomar daño
    [SerializeField] private float tiempoCollision;
    [SerializeField] private float tiempoEntreCollision;

    void Awaken()
    {
        AnimationRapido = false;
    }
    
    void Start()
    {
        AnimationRapido = false;
        playerAnim = GetComponent<Animator>();
        trail = GetComponent<TrailRenderer>();
    }

    
    void Update()
    {

        
        movimientoHorizontal = Input.GetAxisRaw("Horizontal");
        movimientoVertical = Input.GetAxisRaw("Vertical");

        
        float movimientoTotal = Mathf.Abs(movimientoHorizontal) + Mathf.Abs(movimientoVertical);

        // Si hay movimiento, actualiza el Float "Speed" para reproducir la animación
        if (movimientoTotal > 0.5)
        {
            ChangeAnimationLegs(PlayWalkAnim, Legs); 
        }
        else
        {
            // Si no hay movimiento, poner el valor en 0 para detener la animación
            ChangeAnimationLegs(PlayIdle, Legs);
        }

        

        //Lógica de animación de ataque
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }
        if (tiempoSiguienteAtaque <= 0)
        {
            ChangeAnimationState(PlayIdleState);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Disparando();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }

        //Lógica de animación de dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash(Mathf.Abs(movimientoHorizontal) + Mathf.Abs(movimientoVertical));

        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isDashing = false;

        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;


            if (dashCounter <= 0)
            {
                dashCoolCounter = dashCooldown;
                trail.emitting = false;
            }
        }

        if (dashCoolCounter > 0)
        {

            dashCoolCounter -= Time.deltaTime;
        }

        

    }
    //Animaciones
    void ChangeAnimationLegs(string newStateL,string layerName)
    {
        int layerIndex = playerAnim.GetLayerIndex(layerName);
        if (currentStateL == newStateL) return;
        playerAnim.Play(newStateL, layerIndex);
        currentStateL = newStateL;

    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        playerAnim.Play(newState);
        currentState = newState;
    }

    void Movimiento(float mov)
    {

        playerAnim.SetFloat("Horizontal", Mathf.Abs(mov));
        playerAnim.SetFloat("Vertical", Mathf.Abs(mov));

    }
    void Disparando()
    {
        disparando = true;
        
        ChangeAnimationState(PlayShooting);

    }
    
    void Dash(float dir)
    {
        if (dashCoolCounter <= 0 && dashCounter <= 0)
        {
                dashCounter = dashLength;
                playerAnim.SetTrigger("Dash");
                
                trail.emitting = true;
                isDashing = true;
                
                polvoDash.Play(); /*Reproduce particulas*/
        }
    }
    
}
