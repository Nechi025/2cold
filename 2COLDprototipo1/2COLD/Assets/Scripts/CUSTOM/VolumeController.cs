using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // El Slider para ajustar el volumen

    private void Start()
    {
        // Cargar el volumen guardado si existe, de lo contrario usar volumen máximo (1)
        float savedVolume = PlayerPrefs.GetFloat("gameVolume", 1f);
        volumeSlider.value = savedVolume;

        // Aplicar el volumen al iniciar el juego
        AudioListener.volume = savedVolume;

        // Suscribirse al evento de cambio del Slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Método para ajustar el volumen
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        Debug.Log("Volumen ajustado a: " + volume);

        // Guardar el volumen para futuras sesiones
        PlayerPrefs.SetFloat("gameVolume", volume);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento cuando se destruye el objeto
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }
}
