using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    private AudioSource musicSource;

    void Start()
    {
        // Find the persistent music source
        GameObject musicManager = GameObject.FindWithTag("MusicManager");

        if (musicManager != null)
        {
            musicSource = musicManager.GetComponent<AudioSource>();
            volumeSlider.value = musicSource.volume; // Reverse match volume
            volumeSlider.onValueChanged.AddListener(UpdateVolume);
        }
        else
        {
            Debug.LogWarning("MusicManager not found in scene.");
        }
    }

    void UpdateVolume(float value)
    {
        if (musicSource != null)
        {
            musicSource.volume = value;
        }
    }
}