using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    WeaponStats WeaponStats { get; }

    GameObject Bullet { get; }
    //GameObject LASER { get; }
    int Damage { get; }

    int MagSize { get; }

    void Attack();
    void Reaload();

    void ApplyDamageBoost(float boostAmount, float duration);

}