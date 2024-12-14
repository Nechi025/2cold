using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIammo : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    [SerializeField] Shooting shootingScript;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

        // Suscribirse al evento OnAmmoChanged
        if (shootingScript != null)
        {
            shootingScript.OnAmmoChanged += UpdateAmmoUI;
            UpdateAmmoUI(shootingScript.ammo); // Inicializar con el valor actual
        }
    }

    private void UpdateAmmoUI(float currentAmmo)
    {
        textMesh.text = currentAmmo.ToString();
    }

    private void OnDestroy()
    {
        // Desuscribirse para evitar errores
        if (shootingScript != null)
        {
            shootingScript.OnAmmoChanged -= UpdateAmmoUI;
        }
    }
}
