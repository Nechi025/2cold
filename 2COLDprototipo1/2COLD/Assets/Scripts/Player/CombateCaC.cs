using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{

    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;

    [SerializeField] private int dañoGolpe;

    [SerializeField] private float tiempoEntreAtaques;

    [SerializeField] private float tiempoSiguienteAtaque;
    public Animator playerAnim;
    [SerializeField] const string PlayIdleState = "PlayWeaponRifle";
    [SerializeField] const string MeleeAnim = "Melee";
    private string currentState;


    private void Update()
    {
        if(tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
           
        }
        if (Input.GetKey(KeyCode.Mouse1) && tiempoSiguienteAtaque <= 0)
        {
            ChangeAnimationState(MeleeAnim);
            Golpe();
            
            
            SoundManager.Instance.PlaySound("Punch");
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
    }

    void ChangeAnimationState(string newState)
    {
        
        playerAnim.Play(newState);
        currentState = newState;
    }

    private void Golpe()
    {
        
        
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemy"))
            {
                colisionador.transform.GetComponent<Life>().GetDamage(dañoGolpe);

            }
            

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
