using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAmmo : MonoBehaviour
{

    [SerializeField] private Shooting Ammo;
    [SerializeField] private float sumarMunicion = 5f;


    // Update is called once per frame


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //SoundManager.Instance.PlaySound("coin");
            GameManager.Instance.coins--;
            Destroy(gameObject);
            Ammo.ammo += sumarMunicion;
            //puntaje.SumarPuntos(cantidadPuntos);




        }

    }
}
