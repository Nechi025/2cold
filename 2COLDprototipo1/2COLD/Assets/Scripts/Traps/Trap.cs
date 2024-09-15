using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float activeTime = 3f; // Tiempo que la trampa estará activa
    public float inactiveTime = 2f; // Tiempo que la trampa estará inactiva
    public int damage = 20; // Daño que hace la trampa cuando está activa
    private bool isActive = false; // Estado de la trampa (activa o inactiva)

    private SpriteRenderer trapRenderer;
    [SerializeField] private Color activeColor = Color.red; // Color cuando la trampa está activa
    [SerializeField] private Color inactiveColor = Color.gray; // Color cuando la trampa está inactiva

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
            // Si la trampa está activa y el jugador la toca, recibe daño
            LifeS playerLife = collision.GetComponent<LifeS>();
            if (playerLife != null)
            {
                playerLife.GetDamage(damage);
            }
        }
    }
}
