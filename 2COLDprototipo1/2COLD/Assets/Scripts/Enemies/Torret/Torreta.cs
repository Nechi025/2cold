using System.Collections.Generic;
using UnityEngine;

public class Torreta : ManagedUpdateBehavior
{
    public Transform target;
    public float speed = 0f;
    public float rotateSpeed = 0.0025f;
    [SerializeField] private Rigidbody2D rb;
    public float bulletForce;
    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;
    public Transform firingPoint;
    public float fireRate;
    public ObjectPool bulletPool; 
    [SerializeField] private Life Vida;
    [SerializeField] private LineOfSight lineOfSight;

    private float timeToFire = 0.2f;
    private List<BulletData> bullets = new List<BulletData>();

    protected override void Start()
    {
        base.Start(); 
        GameManager.Instance.enemys++;
        GetTarget(); 
    }

    public override void UpdateMe()
    {
        if (!target) GetTarget();

        if (target && IsTargetInSight())
        {
            RotateTowardsTarget();

            if (IsWithinShootingRange() && !GlobalPause.IsPaused())
            {
                Shoot();
            }
        }

        HandleBullets();

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

        // Obtiene una bala del pool
        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = firingPoint.position;
        bullet.transform.rotation = firingPoint.rotation;

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        Vector2 force = -firingPoint.up * bulletForce;

        // Crea un nuevo BulletData para gestionar la lógica de pausa
        var bulletData = new BulletData
        {
            rb = bulletRb,
            originalForce = force,
            storedVelocity = Vector2.zero,
            isPaused = GlobalPause.IsPaused(),
            lifeTime = 2f 
        };

        if (!GlobalPause.IsPaused())
        {
            bulletRb.AddForce(force, ForceMode2D.Impulse);
        }

        bullets.Add(bulletData);
        timeToFire = fireRate;

        SoundManager.Instance.PlaySound("Torreta");
    }

    private void HandleBullets()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            var bulletData = bullets[i];

            if (GlobalPause.IsPaused())
            {
                if (!bulletData.isPaused)
                {
                    bulletData.storedVelocity = bulletData.rb.velocity;
                    bulletData.rb.velocity = Vector2.zero;
                    bulletData.isPaused = true;
                }
            }
            else
            {
                if (bulletData.isPaused)
                {
                    bulletData.isPaused = false;
                    bulletData.rb.velocity = bulletData.storedVelocity;
                    bulletData.rb.AddForce(bulletData.originalForce, ForceMode2D.Impulse);
                }
            }

            if (!GlobalPause.IsPaused()) bulletData.lifeTime -= Time.deltaTime;

            if (bulletData.lifeTime <= 0)
            {
                bulletPool.ReturnObject(bulletData.rb.gameObject);
                bullets.RemoveAt(i);
                i--;
            }
        }
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

    private class BulletData
    {
        public Rigidbody2D rb;
        public Vector2 storedVelocity;
        public Vector2 originalForce;
        public bool isPaused;
        public float lifeTime;
    }
}
