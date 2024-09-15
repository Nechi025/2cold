using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float activeTime = 3f; // Tiempo que la trampa estar� activa
    public float inactiveTime = 2f; // Tiempo que la trampa estar� inactiva
    public int damage = 20; // Da�o que hace la trampa cuando est� activa
    private bool isActive = false; // Estado de la trampa (activa o inactiva)

    private SpriteRenderer trapRenderer;
    [SerializeField] private Color activeColor = Color.red; // Color cuando la trampa est� activa
    [SerializeField] private Color inactiveColor = Color.gray; // Color cuando la trampa est� inactiva

    private void Start()
    {
        trapRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(TrapCycle());
    }

    private IEnumerator TrapCycle()
    {
        while (true)
        {
            // Activar trampa
            isActive = true;
            trapRenderer.color = activeColor;
            yield return new WaitForSeconds(activeTime);

            // Desactivar trampa
            isActive = false;
            trapRenderer.color = inactiveColor;
            yield return new WaitForSeconds(inactiveTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag("Player"))
        {
            // Si la trampa est� activa y el jugador la toca, recibe da�o
            LifeS playerLife = collision.GetComponent<LifeS>();
            if (playerLife != null)
            {
                playerLife.GetDamage(damage);
            }
        }
    }
}
