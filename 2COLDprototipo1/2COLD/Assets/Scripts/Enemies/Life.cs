using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int unitLife = 100;
    public int UnitLife => unitLife;
    public GameObject PatasTorreta;
    public GameObject objetoDrop;


    public void GetDamage(int value)
    {
        unitLife -= value;

        if (unitLife <= 0)
        {

            GameManager.Instance.enemys--;
            if (objetoDrop != null)
            {
                Instantiate(objetoDrop, transform.position, Quaternion.identity);
            }
            Destroy(PatasTorreta);
            Destroy(gameObject);
            
        }
    }


}
