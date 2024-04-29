using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundController : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider masterVolumeSlider, musicVolumeSlider, sfxVolumeSlider;

    private void Start()
    {
        // load current volume to apply across screen
        musicVolumeSlider.value = AudioManager.instance.musicSource.volume;
        sfxVolumeSlider.value = AudioManager.instance.sfxSource.volume;
    }

    public void MusicVolume()
    {
        AudioManager.instance.musicSource.volume = musicVolumeSlider.value;
    }
    public void SfxVolume()
    {
        AudioManager.instance.sfxSource.volume= sfxVolumeSlider.value;
    }
}
