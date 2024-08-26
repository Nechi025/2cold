using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Torreta : ManagedUpdateBehavior
{
    public Transform target;
    public float speed = 0f;
    public float rotateSpeed = 0.0025f;
    [SerializeField] Rigidbody2D rb;
    public GameObject bulletPrefab;
    public float bulletForce;
    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;
    public Transform firingPoint;
    public float fireRate;
    private float timeToFire;
    [SerializeField] Life Vida;

    // Referencia a LineOfSight
    [SerializeField] private LineOfSight lineOfSight;

    // Referencia al CustomUpdateManager
    private CustomUpdateManager customUpdateManager;

    // Start is called before the first frame update
    private void Start()
    {
        GameManager.Instance.enemys++;

        // Registrarse en el CustomUpdateManager
        customUpdateManager = FindObjectOfType<CustomUpdateManager>();
        if (customUpdateManager != null)
        {
            customUpdateManager.Register(this);
        }
    }

    public override void UpdateMe()
    {
        if (!target)
        {
            GetTarget();
        }

        // Verificar si el jugador está dentro del rango y ángulo de visión
        if (target && lineOfSight.CheckRange(target) && lineOfSight.CheckAngle(target) && lineOfSight.CheckView(target))
        {
            RotateTowardsTarget();

            if (Vector2.Distance(target.position, transform.position) <= distanceToShoot)
            {
                if (GlobalPause.IsPaused())
                    return;

                Shoot();
            }
        }

        if (Vida.unitLife <= 0)
        {
            if (customUpdateManager != null)
            {
                customUpdateManager.Unregister(this);
            }
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        if (timeToFire <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(-firingPoint.up * bulletForce, ForceMode2D.Impulse);
            timeToFire = fireRate;
            SoundManager.Instance.PlaySound("Torreta");
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    private void FixedUpdateMe()
    {
        if (target != null && lineOfSight.CheckRange(target) && lineOfSight.CheckAngle(target) && lineOfSight.CheckView(target))
        {
            if (Vector2.Distance(target.position, transform.position) >= distanceToStop)
            {
                rb.velocity = transform.up * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
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

    private void OnDestroy()
    {
        // Desregistrarse del CustomUpdateManager
        if (customUpdateManager != null)
        {
            customUpdateManager.Unregister(this);
        }
    }
}
