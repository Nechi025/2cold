using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float ammo = 10f;

    public float bulletForce = 20f;

    [SerializeField] private List<GameObject> _weaponList;
    [SerializeField] private KeyCode _attack = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reload = KeyCode.R;
    [SerializeField] private KeyCode _weaponSlot1 = KeyCode.Alpha1;
    [SerializeField] private KeyCode _weaponSlot2 = KeyCode.Alpha2;

    [SerializeField] Bullet Damage;
    [SerializeField] private IWeapon _currentWeapon;


    void Start()
    {

        //GameManager.instance.cio++;
        SwitchWeapon(0);
        _currentWeapon.Reaload();
        //PrintWeapon(_currentWeapon);
        //GameManager.instance.cio++;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(_attack))
        {
            //_currentWeapon.Attack();
            Shoot();
           // SoundManager.Instance.PlaySound("shoot");
            //animator.SetBool("Attack", true);

        }

        if (Input.GetKeyDown(_reload)) _currentWeapon.Reaload();



    }

    private void SwitchWeapon(int index)
    {
        foreach (GameObject weapon in _weaponList)
        {
            weapon.gameObject.SetActive(false);
        }
        _weaponList[index].SetActive(true);
        _currentWeapon = _weaponList[index].GetComponent<IWeapon>();

    }

    void Shoot()
    {
        if (ammo > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            ammo--;
        }
        

    }


}
