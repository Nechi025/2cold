using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        
        float savedVolume = PlayerPrefs.GetFloat("gameVolume", 0.5f);
        volumeSlider.value = savedVolume;

        
        AudioListener.volume = savedVolume;

        
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

   
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        

        
        PlayerPrefs.SetFloat("gameVolume", volume);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }
}
