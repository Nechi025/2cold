using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : ManagedUpdateBehavior
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

    Vector2 initialPos;
    [SerializeField] int damage;
    [SerializeField] private Animator Enemy1Anim;

    // Referencia al componente LineOfSight
    [SerializeField] private LineOfSight lineOfSight;

    // Instancia de ObstacleAvoidance2D
    private ObstacleAvoidance obstacleAvoidance;

    [Header("Evitación de obstáculos")]
    [SerializeField] private float avoidanceAngle = 120f;
    [SerializeField] private float avoidanceRadius = 2f;
    [SerializeField] private LayerMask obstacleLayer;

    //[Header("Shooting")]
    //[SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private Transform firingPoint;
    //[SerializeField] private float fireRate = 1f; // Tiempo entre disparos
    //[SerializeField] private float bulletForce = 5f;
    //private float nextTimeToFire = 0f;

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

        // Inicializar ObstacleAvoidance2D
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
        if (target != null && lineOfSight.CheckRange(target) && lineOfSight.CheckAngle(target) && lineOfSight.CheckView(target))
        {
            direccion = target.position - transform.position;
            Vector2 desiredDirection = obstacleAvoidance.GetDir(direccion.normalized);

            posObj = transform.position + (Vector3)desiredDirection * speed * Time.fixedDeltaTime;

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

                    //// Disparar si es tiempo de hacerlo
                    //if (Time.time >= nextTimeToFire)
                    //{
                    //    Shoot();
                    //    nextTimeToFire = Time.time + 1f / fireRate;
                    //}
                }
                else
                {
                    moveToPlayer = false;
                }
            }
        }
        else
        {
            moveToPlayer = false; // Si el jugador no está en rango o ángulo de visión, no se mueve
        }
    }

    //private void Shoot()
    //{
    //    GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    //    Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
    //    rbBullet.AddForce(firingPoint.up * bulletForce, ForceMode2D.Impulse);
    //}

    public void LookDir(Vector2 posA, Vector2 posB)
    {
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
            }
        }
    }
}
