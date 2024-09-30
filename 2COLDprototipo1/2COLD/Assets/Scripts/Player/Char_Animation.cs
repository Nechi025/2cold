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
    //private Vector3 flip;


    [SerializeField] public bool disparando;

    private string currentState;
    private string currentState2;

    const string PlayIdleState = "PlayWeaponRifle";
    const string PlayShooting = "PlayShootingRifle";
    const string PlayIdle = "PlayIdle";
    const string PlayWalkAnim = "PlayMoving";
    //private int combo = 0;

    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;

    //atributos de animaci�n de dash
    [SerializeField] private TrailRenderer trail;
    private float dashCounter;
    private float dashCoolCounter;
    public float dashLength = .5f, dashCooldown = 2f;
    public bool AnimationRapido = false;
    private bool isDashing = false;

    //atributos de animaci�n de tomar da�o
    [SerializeField] private float tiempoCollision;
    [SerializeField] private float tiempoEntreCollision;

    void Awaken()
    {
        AnimationRapido = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        AnimationRapido = false;
        playerAnim = GetComponent<Animator>();
        trail = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        // Obt�n el valor de movimiento en ambos ejes
        movimientoHorizontal = Input.GetAxisRaw("Horizontal");
        movimientoVertical = Input.GetAxisRaw("Vertical");

        // Calcula la magnitud total del movimiento
        float movimientoTotal = Mathf.Abs(movimientoHorizontal) + Mathf.Abs(movimientoVertical);

        // Si hay movimiento, actualiza el Float "Speed" para reproducir la animaci�n
        if (movimientoTotal > 0.5)
        {
            ChangeAnimationState(PlayWalkAnim); // El valor 1 activa la animaci�n de movimiento
        }
        else
        {
            // Si no hay movimiento, poner el valor en 0 para detener la animaci�n
            ChangeAnimationState(PlayIdle);
        }

        //L�gica de particulas
        //Su render volteara cuando el axis sea menor a "0"
        //if (movimientoHorizontal > 0)
        //{
        //    flip = new Vector3(0, 0, 0);
        //}
        //else if (movimientoHorizontal < 0)
        //{
        //    flip = new Vector3(1, 0, 0);
        //}

        //L�gica de animaci�n de ataque
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }
        if (tiempoSiguienteAtaque <= 0)
        {
            ChangeAnimationState2(PlayIdleState);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Disparando();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }

        //L�gica de animaci�n de dash
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

        ////animaci�n de tomar da�o
        //if (tiempoCollision > 0)
        //{
        //    tiempoCollision -= Time.deltaTime;
        //}

        //if (AnimationRapido)
        //{
        //    dashCooldown = 1f;

        //}

    }
    //Animaciones
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        playerAnim.Play(newState);
        currentState = newState;
    }

    void ChangeAnimationState2(string newState2)
    {
        if (currentState2 == newState2) return;
        playerAnim.Play(newState2);
        currentState2 = newState2;
    }

    void Movimiento(float mov)
    {

        playerAnim.SetFloat("Horizontal", Mathf.Abs(mov));
        playerAnim.SetFloat("Vertical", Mathf.Abs(mov));

    }
    void Disparando()
    {
        disparando = true;
        playerAnim.SetTrigger("Disparo");
        ChangeAnimationState2(PlayShooting);

    }
    //void StartCombo()
    //{
    //    atacando = false;
    //    if (combo < 3)
    //    {
    //        combo++;
    //    }
    //}
    //void FinalizarCombo()
    //{
    //    atacando = false;
    //    combo = 0;
    //}
    void Dash(float dir)
    {
        if (dashCoolCounter <= 0 && dashCounter <= 0)
        {
                dashCounter = dashLength;
                playerAnim.SetTrigger("Dash");
                //playerAnim.SetFloat("Horizontal", Mathf.Abs(dir));
                trail.emitting = true;
                isDashing = true;
                //disparando = false; /*desactiva el ataque para evitar errores*/
                //combo = 0; /*vuelve combo a "0" para evitar errores*/
                polvoDash.Play(); /*Reproduce particulas*/
        }
    }
    //public void TomarDano()
    //{
    //    playerAnim.SetTrigger("TakeDamage");
    //    tiempoCollision = tiempoEntreCollision;
    //    //FinalizarCombo();
    //}
}
