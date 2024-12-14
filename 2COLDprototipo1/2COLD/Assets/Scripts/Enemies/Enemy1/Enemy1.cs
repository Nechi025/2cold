using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : ManagedUpdateBehavior
{
    [SerializeField] private float speed = 0.3f;
    private float timerDir = 5f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer flajeloRender;
    [SerializeField] private Transform target;
    [SerializeField] private float tiempoCollision;
    [SerializeField] private float tiempoEntreCollision;
    public bool moveToPlayer;
    private bool isMoving = true; 

    protected Vector3 direccion;
    protected Vector3 posObj;

    [Header("Deteccion de rango")]
    [SerializeField] public float detectRange;

    Vector2 initialPos;
    [SerializeField] int damage;
    [SerializeField] private Animator Enemy1Anim;

    
    [SerializeField] private LineOfSight lineOfSight;

    
    private ObstacleAvoidance obstacleAvoidance;

    [Header("Evitación de obstáculos")]
    [SerializeField] private float avoidanceAngle = 120f;
    [SerializeField] private float avoidanceRadius = 2f;
    [SerializeField] private LayerMask obstacleLayer;

   

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Enemy1Anim = GetComponent<Animator>();
        initialPos = transform.position;
        GameManager.Instance.enemys++;
        if (!target)
        {
            GetTarget();
        }

        
        obstacleAvoidance = new ObstacleAvoidance(transform, avoidanceAngle, avoidanceRadius, obstacleLayer);
    }

    public override void UpdateMe()
    {
        timerDir -= Time.deltaTime;
        if (!target)
        {
            GetTarget();
        }

        if (tiempoCollision > 0)
        {
            tiempoCollision -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (tiempoCollision > 0)
        {
            rb.velocity = Vector2.zero; 
            return; 
        }

        if (target != null && lineOfSight.CheckRange(target) && lineOfSight.CheckAngle(target) && lineOfSight.CheckView(target))
        {
            direccion = target.position - transform.position;
            Vector2 desiredDirection = obstacleAvoidance.GetDir(direccion.normalized);

            posObj = transform.position + (Vector3)desiredDirection * speed * Time.fixedDeltaTime;

            if (direccion.magnitude < detectRange)
            {
                if (target != null && !GlobalPause.IsPaused())
                {
                    moveToPlayer = true;
                    rb.MovePosition(posObj);
                    LookDir(target.position, transform.position);
                }
                else
                {
                    moveToPlayer = false;
                }
            }
        }
        else
        {
            moveToPlayer = false; // No se mueve si no está en rango
        }
    }


    

    public void LookDir(Vector2 posA, Vector2 posB)
    {
        if (tiempoCollision > 0) return; // Evitar rotación mientras está en cooldown

        Vector2 lookDir = posA - posB;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
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

                // Detener el movimiento del enemigo
                rb.velocity = Vector2.zero;

                // Congelar la rotación del enemigo
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;

                
                StartCoroutine(ResetRotation());
                StartCoroutine(ResetMovement());
            }
        }
    }

    private IEnumerator ResetRotation()
    {
        yield return new WaitForSeconds(tiempoCollision); // Esperar el tiempo de cooldown

        // Reactivar la rotación del enemigo
        rb.constraints = RigidbodyConstraints2D.None;
    }

    private IEnumerator ResetMovement()
    {
        yield return new WaitForSeconds(tiempoCollision); // Esperar el tiempo de cooldown
        isMoving = true; // Volver a permitir movimiento
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);

       
        Vector3 leftLimit = Quaternion.Euler(0, 0, -avoidanceAngle / 2) * transform.right * avoidanceRadius;
        Vector3 rightLimit = Quaternion.Euler(0, 0, avoidanceAngle / 2) * transform.right * avoidanceRadius;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftLimit);
        Gizmos.DrawLine(transform.position, transform.position + rightLimit);

        
        if (obstacleAvoidance != null)
        {
            obstacleAvoidance.DrawGizmos();
        }
    }
}
