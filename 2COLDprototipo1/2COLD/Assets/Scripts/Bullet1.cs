using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Bullet1 : MonoBehaviour, IBullet
{
    [SerializeField] public float _speed = 5f;
    [SerializeField] private float _lifeTime = 2f;
    [SerializeField] public int damage;
    [SerializeField] private List<GameObject> _weaponList;
    [SerializeField] private IWeapon _currentWeapon;

    [SerializeField] private LayerMask _hitteableLayer;
    [SerializeField] private IWeapon _owner;
    public float bulletForce = 20f;
    public Transform firePoint;


    public float Speed => _speed;

    public float LifeTime => _lifeTime;
    public LayerMask HitteableLayer => _hitteableLayer;
    private Vector3 shootingDirection;
    public IWeapon Owner => _owner;



    private Collider2D _collider;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentWeapon = (IWeapon)firePoint;
        Init();
    }

    private void Update()
    {
        Travel();
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0) Destroy(this.gameObject);





    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

        if (collision.transform.GetComponent<LifeS>())
        {
            LifeS life = collision.transform.GetComponent<LifeS>();

            life.GetDamage(damage);
        }
    }



    public void Travel()
    {
        // Mueve la bala en la dirección almacenada.
        //transform.position += (Vector3)shootingDirection.normalized * _speed * Time.deltaTime;
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    public void Init()
    {


        //_rigidbody.isKinematic = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

    }

    public void SetOwner(IWeapon weapon) => _owner = weapon;

    public IProduct Clone()
    {
        return Instantiate(this, firePoint.position, firePoint.rotation);
    }

    public void SetShootingDirection(Vector2 direction)
    {
        shootingDirection = direction;

        // Llama al nuevo método para ajustar la dirección de la bala.
        SetBulletDirection();
    }

    private void SetBulletDirection()
    {
        // Calcula el ángulo de rotación basado en la dirección del disparo.
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;

        // Rotaciona la bala para apuntar en la dirección del disparo.
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    public GameObject MyGameObject => gameObject;
}


