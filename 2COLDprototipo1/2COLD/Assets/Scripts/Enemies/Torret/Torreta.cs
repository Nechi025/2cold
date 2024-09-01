using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : ManagedUpdateBehavior
{
    public Transform target;
    public float speed = 0f;
    public float rotateSpeed = 0.0025f;
    [SerializeField] private Rigidbody2D rb;
    public GameObject bulletPrefab;
    public float bulletForce;
    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;
    public Transform firingPoint;
    public float fireRate;
    private float timeToFire;
    [SerializeField] private Life Vida;
    [SerializeField] private LineOfSight lineOfSight;

    protected override void Start()
    {
        base.Start();  // Llamamos al Start de ManagedUpdateBehavior
        GameManager.Instance.enemys++;

        // Obtener el target inicial al inicio
        GetTarget();
    }

    public override void UpdateMe()
    {
        if (!target) GetTarget();

        if (target && IsTargetInSight())
        {
            RotateTowardsTarget();

            if (IsWithinShootingRange())
            {
                if (!GlobalPause.IsPaused()) Shoot();
            }
        }

        if (Vida.unitLife <= 0) DestroyTorreta();
    }

    private bool IsTargetInSight()
    {
        return lineOfSight.CheckRange(target) && lineOfSight.CheckAngle(target) && lineOfSight.CheckView(target);
    }

    private bool IsWithinShootingRange()
    {
        return Vector2.Distance(target.position, transform.position) <= distanceToShoot;
    }

    private void Shoot()
    {
        if (timeToFire > 0)
        {
            timeToFire -= Time.deltaTime;
            return;
        }

        var bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        var bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(-firingPoint.up * bulletForce, ForceMode2D.Impulse);
        timeToFire = fireRate;
        SoundManager.Instance.PlaySound("Torreta");
    }

    private void FixedUpdate()
    {
        if (target == null || !IsTargetInSight()) return;

        rb.velocity = Vector2.Distance(target.position, transform.position) >= distanceToStop ? transform.up * speed : Vector2.zero;
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = (Vector2)(target.position - transform.position);
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, angle), rotateSpeed);
    }

    private void GetTarget()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        target = player != null ? player.transform : null;
    }

    private void DestroyTorreta()
    {
        CustomUpdateManager.Instance?.Unregister(this);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        CustomUpdateManager.Instance?.Unregister(this);
    }
}
