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

    Vector2 initialPos;
    [SerializeField] int damage;
    [SerializeField] private Animator Enemy1Anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Enemy1Anim = GetComponent<Animator>();
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

        if (tiempoCollision > 0)
        {
            tiempoCollision -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
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
            else
            {
                moveToPlayer = false;
            }
        }
    }

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
