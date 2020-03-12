using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer = null;
    Slider VolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        VolumeSlider = GetComponent<Slider>();
        if (this.gameObject.name == "MasterSlider") //初期設定
        {
            audioMixer.SetFloat("MasterVol", VolumeSlider.value);
        }
        if (this.gameObject.name == "BGMSlider")   //初期設定
        {
            audioMixer.SetFloat("BGMVol", VolumeSlider.value);
        }
        if (this.gameObject.name == "SESlider")    //初期設定
        {
            audioMixer.SetFloat("SEVol", VolumeSlider.value);
        }
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
