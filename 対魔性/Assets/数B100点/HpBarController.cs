using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HpBarController : MonoBehaviour
{
    GameObject Player;
    PlayerContloller PlayerContloller;
    Slider slider;
    public int Hp;
    int MaxHp;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerContloller = Player.GetComponent<PlayerContloller>();
        slider = GameObject.Find("PlayerHealth").GetComponent<Slider>();
        MaxHp = PlayerContloller.MaxPlayerHp;
        slider.maxValue = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {   
        Hp = PlayerContloller.PlayerHp;
        slider.value = Hp;
    }
}
