﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    public static float mas=100.0f, se=100.0f, bgm=100.0f; //初期値(適当)
    [SerializeField] private AudioMixer audioMixer = null;
    Slider VolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        VolumeSlider = GetComponent<Slider>();
        if (this.gameObject.name == "MasterSlider") //初期設定
        {
            //ゲームシーン移動用
            if (mas == 100.0f) //初期値の時
            {
                mas = VolumeSlider.value;

            }
            else
            {
                VolumeSlider.value = mas;
            }

            audioMixer.SetFloat("MasterVol", mas);
        }
        if (this.gameObject.name == "BGMSlider")   //初期設定
        {
            if (bgm == 100.0f)
            {
                bgm = VolumeSlider.value;

            }
            else
            {
                VolumeSlider.value = bgm;
            }
            audioMixer.SetFloat("BGMVol", bgm);
        }
        if (this.gameObject.name == "SESlider")    //初期設定
        {
            if (se == 100.0f)
            {
                se = VolumeSlider.value;

            }
            else
            {
                VolumeSlider.value = se;
            }
            audioMixer.SetFloat("SEVol", se);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetMaster()
    {
        mas = VolumeSlider.value;
        audioMixer.SetFloat("MasterVol", VolumeSlider.value);
    }

    public void SetBGM()
    {
        bgm = VolumeSlider.value;
        audioMixer.SetFloat("BGMVol", VolumeSlider.value);
    }

    public void SetSE()
    {
        se = VolumeSlider.value;
        audioMixer.SetFloat("SEVol", VolumeSlider.value);
    }
}
