using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int unitLife = 100;
    public int UnitLife => unitLife;
    


    public void GetDamage(int value)
    {
        unitLife -= value;

        if (unitLife <= 0)
        {

            GameManager.Instance.enemys--;
            Destroy(gameObject);
            
        }
    }


}
