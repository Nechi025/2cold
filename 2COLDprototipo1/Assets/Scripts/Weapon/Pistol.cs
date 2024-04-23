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
        // Puedes aplicar la l�gica para aumentar el da�o de la pistola durante un tiempo aqu�
        StartCoroutine(ResetDamageAfterDelay(boostAmount, duration));
    }

    private IEnumerator ResetDamageAfterDelay(float boostAmount, float duration)
    {
        // Aplicar el aumento de da�o
        _weaponStats.Damage += (int)boostAmount;

        // Esperar la duraci�n del aumento de da�o
        yield return new WaitForSeconds(duration);

        // Restablecer el da�o despu�s de la duraci�n
        _weaponStats.Damage -= (int)boostAmount;
    }



    public void Attack()
    {
        if (_bulletCount > 0)
        {
            // Obt�n la direcci�n de apuntado del personaje (rotaci�n en el eje Y).
            float characterRotationY = transform.rotation.eulerAngles.y;

            // Calcula la direcci�n del disparo en funci�n de la rotaci�n del personaje.
            Vector3 shootingDirection = Quaternion.Euler(0, characterRotationY, 0) * Vector3.forward;

            Bullet bullet = bulletFactory.CreateProduct();
            bullet.transform.position = transform.position;

            // Establece la direcci�n del disparo.
            bullet.SetShootingDirection(shootingDirection);

            bullet.SetOwner(this);
            _bulletCount--;
        }
    }

    public void Reaload() => _bulletCount = MagSize;


}
