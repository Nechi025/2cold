using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float liveTime = 3f;
    [SerializeField] public int damage;

    void FixedUpdate()
    {
        if (GlobalPause.IsPaused())
            return;
        liveTime -= 6f * Time.deltaTime;

        if (liveTime <= 0)
        {
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

        if (collision.transform.GetComponent<Life>())
        {
            Life life = collision.transform.GetComponent<Life>();
            
            life.GetDamage(damage);
        }
    }
}
