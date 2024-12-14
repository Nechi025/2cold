using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempBar : MonoBehaviour
{
    public Slider barraTemp;
    [SerializeField] PlayerMovement tiempoQuieto;
    [SerializeField] float maxTime;

    private void Start()
    {
        maxTime = tiempoQuieto.timer;
    }

    private void Update()
    {
        
        barraTemp.value = (float)tiempoQuieto.timer / (float)maxTime;

    }

}
