using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedChanger : MonoBehaviour
{
    //  おまんこ＾～
    //[SerializeField] float gamespeed = 0;
    [SerializeField] Slider slider = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeGameSpeed()
    {
        Time.timeScale = slider.value;
    }
}
