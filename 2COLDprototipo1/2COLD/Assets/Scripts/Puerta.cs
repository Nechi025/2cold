using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{

    //public GameObject key;
    //public GameObject keys;
    public GameObject pasaje;
    //public GameObject Texto1;
    //public GameObject DialogoActive2;
    //public GameObject Circle;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //SoundManager.Instance.PlaySound("damage");
            //SoundManager.Instance.PlaySound("CloseDoor");
            //key.SetActive(true);
            //keys.SetActive(true);
            pasaje.SetActive(false);
            //Texto1.SetActive(false);
            //DialogoActive2.SetActive(true);
            //Circle.SetActive(false);



        }

    }
}
