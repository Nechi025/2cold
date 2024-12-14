using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float activeTime = 3f;
    [SerializeField] private float inactiveTime = 2f;
    [SerializeField] private int damage = 20; 
    private bool isActive = false; 

    private SpriteRenderer trapRenderer;
    [SerializeField] private Color activeColor = Color.red; 
    [SerializeField] private Color inactiveColor = Color.gray;

    private void Start()
    {
        trapRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(TrapCycle());
    }

    private IEnumerator TrapCycle()
    {
        while (true)
        {
            
            isActive = true;
            trapRenderer.color = activeColor;
            yield return new WaitForSeconds(activeTime);

           
            isActive = false;
            trapRenderer.color = inactiveColor;
            yield return new WaitForSeconds(inactiveTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag("Player"))
        {
            
            LifeS playerLife = collision.GetComponent<LifeS>();
            if (playerLife != null)
            {
                playerLife.GetDamage(damage);
            }
        }
    }
}
