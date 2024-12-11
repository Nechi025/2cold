using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{

    
    [SerializeField] private GameObject pasaje;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            pasaje.SetActive(false);
            Destroy(gameObject);
        }

    }
}
