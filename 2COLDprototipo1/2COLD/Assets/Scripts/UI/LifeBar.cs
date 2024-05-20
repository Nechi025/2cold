using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Slider barraHP;
    [SerializeField] LifeS playerLife;
    [SerializeField] int maxLife;

    private void Start()
    {
        maxLife = playerLife.unitLifes;
    }

    private void Update()
    {
        // print(bar.fillAmount);

        // bar.fillAmount = (float)playerLife.unitLife / (float)maxLife;
        barraHP.value = (float)playerLife.unitLifes / (float)maxLife;

    }

}