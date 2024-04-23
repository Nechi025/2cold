using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{

    [SerializeField] public int _bulletCount;
    [SerializeField] private WeaponStats _weaponStats;
    private BulletFactory bulletFactory;

    private int _currentDamage;

    public GameObject Bullet => _weaponStats.Bullet;
    //public GameObject LASER => _weaponStats.LASER;

    public int Damage => _currentDamage;

    public int MagSize => _weaponStats.MagSize;

    public WeaponStats WeaponStats => _weaponStats;


    private void Start()
    {
        Bullet bullet = _weaponStats.Bullet.GetComponent<Bullet>();
        bulletFactory = new BulletFactory(bullet);
        _currentDamage = _weaponStats.Damage;
        Reaload();
    }

    public void ApplyDamageBoost(float boostAmount, float duration)
    {
        // Puedes aplicar la lógica para aumentar el daño de la pistola durante un tiempo aquí
        StartCoroutine(ResetDamageAfterDelay(boostAmount, duration));
    }

    private IEnumerator ResetDamageAfterDelay(float boostAmount, float duration)
    {
        // Aplicar el aumento de daño
        _weaponStats.Damage += (int)boostAmount;

        // Esperar la duración del aumento de daño
        yield return new WaitForSeconds(duration);

        // Restablecer el daño después de la duración
        _weaponStats.Damage -= (int)boostAmount;
    }



    public void Attack()
    {
        if (_bulletCount > 0)
        {
            // Obtén la dirección de apuntado del personaje (rotación en el eje Y).
            float characterRotationY = transform.rotation.eulerAngles.y;

            // Calcula la dirección del disparo en función de la rotación del personaje.
            Vector3 shootingDirection = Quaternion.Euler(0, characterRotationY, 0) * Vector3.forward;

            Bullet bullet = bulletFactory.CreateProduct();
            bullet.transform.position = transform.position;

            // Establece la dirección del disparo.
            bullet.SetShootingDirection(shootingDirection);

            bullet.SetOwner(this);
            _bulletCount--;
        }
    }

    public void Reaload() => _bulletCount = MagSize;


}
