using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIammo : MonoBehaviour
{

    private TextMeshProUGUI textMesh;
    [SerializeField] Shooting municion;
    [SerializeField] float maxAmmo;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        maxAmmo = municion.ammo;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        maxAmmo = municion.ammo;
        textMesh.text = maxAmmo.ToString();
    }


}