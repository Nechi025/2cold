using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEspectro : MonoBehaviour
{
    [SerializeField] private float speed = 0.3f;
    private float timerDir = 5f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer flajeloRender;
    [SerializeField] private Transform target;
    [SerializeField] private float tiempoCollision;
    [SerializeField] private float tiempoEntreCollision;
    public bool moveToPlayer;
    

    protected Vector3 direccion;
    protected Vector3 posObj;


    [Header("Deteccion de rango")]
    [SerializeField] public float detectRange;


    
    Vector2 initialPos;

  

    
    [SerializeField] int damage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        initialPos = transform.position;
        GameManager.Instance.enemys++;



    }

    private void Update()
    {
        

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
                if (!GlobalPause.IsPaused())
                    return;

                moveToPlayer = true;
                rb.MovePosition(posObj);
                LookDir(target.position, transform.position);
            }
            else moveToPlayer = false;
        }

       

    }


    //Mira en la dirección que va a caminar
    void LookDir(Vector2 posA, Vector2 posB)
    {
        

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
            
        }
        else if (!GameObject.FindGameObjectWithTag("Player"))
        {
            target = null;
        }
    }



    
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


   
}
