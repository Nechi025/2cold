using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{

    public Transform target;
    public float speed = 0f;
    public float rotateSpeed = 0.0025f;
    [SerializeField] Rigidbody2D rb;
    public GameObject bulletPrefab;
    public float bulletForce;
    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;

    public Transform firingPoint;

    public float fireRate;
    private float timeToFire;

    [SerializeField] Life Vida;

    // Start is called before the first frame update
    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        GameManager.Instance.enemys++;

    }

    // Update is called once per frame

    //Recibimos un target y rotamos para apuntarle y dispararle
    private void Update()
    {
        if (!target)
        {
            GetTarget();

        }
        else
        {
            RotateTowardsTarget();
        }

        if (Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            if (GlobalPause.IsPaused())
                return;
            Shoot();
        }

        if (Vida.unitLife <= 0)
        {
            Destroy(gameObject);
        }
    }



    private void Shoot()
    {
        if (timeToFire <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            //SoundManager.Instance.PlaySound("MogfrudShoot");
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(-firingPoint.up * bulletForce, ForceMode2D.Impulse);
            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    // Si no tiene target no dispara
    private void FixedUpdate()
    {
        if (target != null)
        {
            if (Vector2.Distance(target.position, transform.position) >= distanceToStop)
            {


                rb.velocity = transform.up * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }


    }

    //Gira hacia el Target
    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    //Consigue un target solo
    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            // transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (!GameObject.FindGameObjectWithTag("Player"))
        {
            target = null;
        }
    }



}
