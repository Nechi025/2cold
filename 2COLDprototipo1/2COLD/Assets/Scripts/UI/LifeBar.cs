using System;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Slider barraHP;
    [SerializeField] private LifeS playerLife;
    [SerializeField] private int maxLife;

    private void Start()
    {
        // Inicializamos el valor máximo de vida
        maxLife = playerLife.unitLifes;
        //barraHP.maxValue = maxLife;

        // Actualizamos el valor inicial de la barra de vida
        //barraHP.value = playerLife.unitLifes;

        // Suscribimos un método al evento OnLifeChanged
        playerLife.OnLifeChanged += UpdateLifeBar;
    }

    private void OnDestroy()
    {
        // Desuscribimos el método del evento para evitar errores al destruir el objeto
        playerLife.OnLifeChanged -= UpdateLifeBar;
    }

    private void UpdateLifeBar(int currentLife)
    {
        // Actualizamos el valor de la barra de vida
        barraHP.value = (float)playerLife.unitLifes / (float)maxLife;
    }
}
