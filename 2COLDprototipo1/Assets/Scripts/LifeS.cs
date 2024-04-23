using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LifeS : MonoBehaviour
{
    public int unitLifes;
    public int UnitLifes => unitLifes;

    [SerializeField] Animator playerAnim;

    void Start()
    {
        unitLifes = 100;

        playerAnim = GetComponent<Animator>();
    }

    //Vida del player que recibe daño
    public void GetDamage(int value)
    {
        //SoundManager.Instance.PlaySound("Body_Impact");
        unitLifes -= value;
        if (unitLifes <= 0)
        {
            //GameManager.Instance.cio--;
            playerAnim.SetTrigger("Death");

        }
    }
    public void Death()
    {
        SceneManager.LoadScene(9);
        Destroy(gameObject);
    }


    public void GetHealth(int value)
    {
        unitLifes += value;

    }


}