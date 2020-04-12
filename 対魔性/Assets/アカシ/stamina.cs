using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stamina : MonoBehaviour
{
    GameObject Player,ThisgameObject;
    PlayerContloller PlayerContloller;
    Slider slider;
    public float Width;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerContloller = Player.GetComponent<PlayerContloller>();
        slider = GetComponent<Slider>();
        slider.maxValue = PlayerContloller.MAX_STAMINA;
        Width = 200.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //slider.maxValue = PlayerContloller.MAX_STAMINA;
        //GetComponent<RectTransform>().sizeDelta = new Vector2(Width, 30);
        slider.value = PlayerContloller.Dashstamina;
    }
}
