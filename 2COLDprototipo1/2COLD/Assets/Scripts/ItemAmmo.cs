using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAmmo : MonoBehaviour
{
    [SerializeField] private Shooting ammoController;
    [SerializeField] private float sumarMunicion = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
            ammoController.AddAmmo(sumarMunicion);
        }
    }
}
