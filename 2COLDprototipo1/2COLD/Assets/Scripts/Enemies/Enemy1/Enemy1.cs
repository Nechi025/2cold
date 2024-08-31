using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : ManagedUpdateBehavior
{
    [Header("Movement")]
    public float speed = 0.3f;
    [SerializeField] private float detectRange = 10f;

    [Header("Collision Settings")]
    [SerializeField] private float tiempoCollision = 0.5f;
    [SerializeField] private float tiempoEntreCollision = 0.5f;
    [SerializeField] private int damage = 10;
    private float collisionTimer;

    [Header("Obstacle Avoidance")]
    [SerializeField] private float avoidanceAngle = 120f;
    [SerializeField] private float avoidanceRadius = 2f;
    [SerializeField] private LayerMask obstacleLayer;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineOfSight lineOfSight;
    [SerializeField] private Animator enemyAnimator;

    private ObstacleAvoidance obstacleAvoidance;
    private Transform target;
    private Vector3 posObj;
    private Vector3 initialPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        initialPos = transform.position;
        obstacleAvoidance = new ObstacleAvoidance(transform, avoidanceAngle, avoidanceRadius, obstacleLayer);

        GameManager.Instance.enemys++;
        GetTarget();
    }

    public override void UpdateMe()
    {
        collisionTimer -= Time.deltaTime;

        if (!target)
        {
            GetTarget();
        }
    }

    private void FixedUpdate()
    {
        if (target == null || !lineOfSight.CheckRange(target) || !lineOfSight.CheckAngle(target) || !lineOfSight.CheckView(target))
        {
            return; // Early exit if no target or not within sight
        }

        Vector3 direction = (target.position - transform.position).normalized;
        Vector2 adjustedDirection = obstacleAvoidance.GetDir(direction);
        posObj = transform.position + (Vector3)adjustedDirection * speed * Time.fixedDeltaTime;

        if (Vector2.Distance(transform.position, target.position) < detectRange)
        {
            if (GlobalPause.IsPaused())
                return;

            rb.MovePosition(posObj);
            LookDir(target.position);
        }
    }

    private void LookDir(Vector2 targetPos)
    {
        Vector2 lookDir = targetPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void GetTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player ? player.transform : null;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collisionTimer > 0 || collision.gameObject.layer != 9)
        {
            return;
        }

        LifeS life = collision.transform.GetComponent<LifeS>();
        if (life != null)
        {
            life.GetDamage(damage);
            collisionTimer = tiempoEntreCollision;
        }
    }
}
