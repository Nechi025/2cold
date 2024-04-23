using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private float cantidadPuntos;
    //[SerializeField] private Puntaje puntaje;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.coins++;
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
            GameManager.Instance.coins--;
            Destroy(gameObject);
            //puntaje.SumarPuntos(cantidadPuntos);




        }

    }



}
