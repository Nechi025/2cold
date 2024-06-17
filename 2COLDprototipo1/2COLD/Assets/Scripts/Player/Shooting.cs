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

    private List<BulletData> bullets = new List<BulletData>();

    void Start()
    {
        SwitchWeapon(0);
        _currentWeapon.Reaload();
    }

    void Update()
    {
        if (Input.GetKeyDown(_attack))
        {
            Shoot();
            SoundManager.Instance.PlaySound("Bullet");
        }

        if (Input.GetKeyDown(_reload))
        {
            _currentWeapon.Reaload();
        }

        // Pausar o reanudar las balas según el estado global de pausa
        for (int i = 0; i < bullets.Count; i++)
        {
            var bulletData = bullets[i];

            if (GlobalPause.IsPaused())
            {
                // Almacena la velocidad actual y detiene la bala
                if (!bulletData.isPaused)
                {
                    bulletData.storedVelocity = bulletData.rb.velocity;
                    bulletData.rb.velocity = Vector2.zero;
                    bulletData.isPaused = true;
                }
            }
            else
            {
                // Restaura la velocidad almacenada
                if (bulletData.isPaused)
                {
                    bulletData.isPaused = false;

                    // Restaura la velocidad almacenada
                    bulletData.rb.velocity = bulletData.storedVelocity;

                    // Aplica la fuerza original una vez para asegurar que se mueve
                    bulletData.rb.AddForce(bulletData.originalForce, ForceMode2D.Impulse);
                }
            }

            // Reducir el tiempo de vida de la bala
            bulletData.lifeTime -= Time.deltaTime;
            if (bulletData.lifeTime <= 0)
            {
                Destroy(bulletData.rb.gameObject);
                bullets.RemoveAt(i);
                i--; // Decrementar el índice para no saltar el siguiente elemento
            }
        }
    }


    // Método para sumar más balas a la munición
    public void AddAmmo(float amount)
    {
        ammo += amount;
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
            Vector2 force = firePoint.up * bulletForce;

            // Agregar la bala a la lista de balas
            var bulletData = new BulletData
            {
                rb = rb,
                originalForce = force,
                storedVelocity = Vector2.zero,
                isPaused = GlobalPause.IsPaused(),
                lifeTime = 2f
            };

            // Si el juego no está en pausa, aplicar fuerza a la bala
            if (!GlobalPause.IsPaused())
            {
                rb.AddForce(force, ForceMode2D.Impulse);
            }

            bullets.Add(bulletData);
            ammo--;
        }
    }

    // Clase para almacenar datos de las balas
    private class BulletData
    {
        public Rigidbody2D rb;
        public Vector2 storedVelocity;
        public Vector2 originalForce;
        public bool isPaused;
        public float lifeTime;
    }
}
