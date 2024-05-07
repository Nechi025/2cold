using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float speed = 0.3f;
    private float timerDir = 5f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer flajeloRender;
    public Transform target;
    [SerializeField] private float tiempoCollision;
    [SerializeField] private float tiempoEntreCollision;
    public bool moveToPlayer;
    

    protected Vector3 direccion;
    protected Vector3 posObj;


    [Header("Deteccion de rango")]
    [SerializeField] public float detectRange;


    //posición inicial del alien
    Vector2 initialPos;

  

    //Daño que realiza
    [SerializeField] int damage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //se guarda posicion
        initialPos = transform.position;
        GameManager.Instance.enemys++;



    }

    private void Update()
    {
        //Vector2 num1;
        //Vector2 num2;

        //float setTimerDir = 5f;

        timerDir -= Time.deltaTime;
        if (!target)
        {
            GetTarget();

        }

        //Para que no este constantemente haciendo daño tiene un cooldown
        if (tiempoCollision > 0)
        {
            tiempoCollision -= Time.deltaTime;
        }

    }
    //Actualzia el estado de la vida del Alien


    private void FixedUpdate()
    {
        //Apunta al objetivo y se mueve hacia él
        direccion = target.position - transform.position;
        posObj = transform.position + direccion * speed * Time.fixedDeltaTime;
        if (direccion.magnitude < detectRange)
        {
            if (!target)
            {
                GetTarget();
            }
            else if (target != null)
            {
                if (GlobalPause.IsPaused())
                    return;

                moveToPlayer = true;
                rb.MovePosition(posObj);
                LookDir(target.position, transform.position);
            }
            else moveToPlayer = false;
        }

        /*else if (target == null && transform.position.x != initialPos.x && transform.position.y != initialPos.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, initialPos, speed * Time.deltaTime);
            LookDir(initialPos, transform.position);
        }*/

    }


    //Mira en la dirección que va a caminar
    void LookDir(Vector2 posA, Vector2 posB)
    {
        //Vector2 lookDir = posA - posB;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x);
        //rb.rotation = angle;

        if (posA.x <= posB.x)
        {
            flajeloRender.flipX = true;
        }
        else if (posA.x >= posB.x)
        {
            flajeloRender.flipX = false;
        }


    }


    //Recibe el target
    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            // transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (!GameObject.FindGameObjectWithTag("Player"))
        {
            target = null;
        }
    }



    //logica de daño del Alien
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (tiempoCollision <= 0)
        {
            if (collision.gameObject.layer == 9)
            {
                LifeS life = collision.transform.GetComponent<LifeS>();
                life.GetDamage(damage);
                tiempoCollision = tiempoEntreCollision;


            }
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        
    }
}
