using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLevel2 : MonoBehaviour
{

    [SerializeField] private float cantidadPuntos;
    //[SerializeField] private Puntaje puntaje;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.coinsLevel2++;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //SoundManager.Instance.PlaySound("coin");
            GameManager.Instance.coinsLevel2--;
            Destroy(gameObject);
            //puntaje.SumarPuntos(cantidadPuntos);




        }

    }



}