using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "Stats/WeaponStats", order = 0)]
public class WeaponStats : ScriptableObject
{

    [SerializeField] private int _damage;

    [field: SerializeField] public GameObject Bullet { get; private set; }
    [field: SerializeField]

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    [field: SerializeField] public int MagSize { get; private set; } = 15;





}
