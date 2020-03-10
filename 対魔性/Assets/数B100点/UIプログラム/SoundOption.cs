using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    Slider VolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        VolumeSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetMaster()
    {
        audioMixer.SetFloat("MasterVol", VolumeSlider.value);
    }

    public void SetBGM()
    {
        audioMixer.SetFloat("BGMVol", VolumeSlider.value);
    }

    public void SetSE()
    {
        audioMixer.SetFloat("SEVol", VolumeSlider.value);
    }
}
