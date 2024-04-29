using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected GameObject _bullet;
    //[SerializeField] protected GameObject _laser;

    [SerializeField] protected float _damage;
    [SerializeField] protected int _magSize;
    [SerializeField] protected int _bulletCount;

    public GameObject Bullet => _bullet;
    //public GameObject LASER => _laser;

    public float Damage => _damage;

    public int MagSize => _magSize;

    public WeaponStats WeaponStats => throw new System.NotImplementedException();

    int IWeapon.Damage => throw new System.NotImplementedException();

    public void ApplyDamageBoost(float boostAmount, float duration)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Attack() => Debug.Log("Shoot");

    public void Reaload() => _bulletCount = _magSize;

}
