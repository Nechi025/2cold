using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public float ammo = 10f;
    public float bulletForce = 20f;
    [SerializeField] private KeyCode _attack = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reload = KeyCode.R;
    [SerializeField] private ObjectPool bulletPool;  

    private List<BulletData> bullets = new List<BulletData>();

    void Update()
    {
        if (MenuPausa.isGamePaused)
            return; // Evitar disparar cuando el juego está en pausa

        if (Input.GetKeyDown(_attack))
        {
            Shoot();
        }

        if (Input.GetKeyDown(_reload))
        {
            // Implementar la lógica de recarga si es necesario
        }

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

            bulletData.lifeTime -= Time.deltaTime;
            if (bulletData.lifeTime <= 0)
            {
                bulletPool.ReturnObject(bulletData.rb.gameObject);  // Devuelve la bala al pool
                bullets.RemoveAt(i);
                i--;
            }
        }
    }


    // Método para sumar más balas a la munición
    public void AddAmmo(float amount)
    {
        ammo += amount;
    }


    void Shoot()
    {
        if (MenuPausa.isGamePaused)
            return; // No disparar si el juego está en pausa

        if (ammo > 0)
        {
            SoundManager.Instance.PlaySound("Bullet");
            GameObject bullet = bulletPool.GetObject();  
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 force = firePoint.up * bulletForce;

            var bulletData = new BulletData
            {
                rb = rb,
                originalForce = force,
                storedVelocity = Vector2.zero,
                isPaused = GlobalPause.IsPaused(),
                lifeTime = 2f
            };

            if (!GlobalPause.IsPaused())
            {
                rb.AddForce(force, ForceMode2D.Impulse);
            }

            bullets.Add(bulletData);
            ammo--;
        }
        else if (ammo == 0)
        {
            SoundManager.Instance.PlaySound("NoBullet");
        }
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

