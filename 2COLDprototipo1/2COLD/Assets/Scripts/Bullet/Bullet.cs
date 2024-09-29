using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] public float _speed = 5f;
    [SerializeField] private float _lifeTime = 2f; // Tiempo de vida original de la bala
    private float _currentLifeTime;  // Variable para manejar el tiempo de vida actual
    [SerializeField] public int damage;
    [SerializeField] private LayerMask _hittableLayer;
    [SerializeField] private IWeapon _owner;
    public float bulletForce = 20f;
    public Transform firePoint;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // Resetear el tiempo de vida cada vez que la bala se activa
        _currentLifeTime = _lifeTime;
    }

    private void Update()
    {
        // Reducir el tiempo de vida actual de la bala
        _currentLifeTime -= Time.deltaTime;
        if (_currentLifeTime <= 0)
        {
            // En lugar de destruir, desactivar la bala para devolverla al pool
            gameObject.SetActive(false);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Life>())
        {
            Life life = collision.transform.GetComponent<Life>();
            life.GetDamage(damage);
        }

        // Desactivar la bala al colisionar con algo
        gameObject.SetActive(false);
    }
}
