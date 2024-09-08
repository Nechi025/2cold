using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 9 && PlayerMovement.Instance.isDashing) || collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
    }
}