using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HpBarController : MonoBehaviour
{
    
    GameObject Player,ThisgameObject;
    PlayerContloller PlayerContloller;
    Slider slider;
    public int Hp;
    int MaxHp;
    int Width; //HPバーのサイズ
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerContloller = Player.GetComponent<PlayerContloller>();
        ThisgameObject = GameObject.Find("PlayerHealth");
        slider = GameObject.Find("PlayerHealth").GetComponent<Slider>();
        MaxHp = PlayerContloller.MaxPlayerHp;
        slider.maxValue = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        Width = PlayerContloller.MaxPlayerHp * 20; //横幅=最大体力かけ20
        ThisgameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Width,30); //HPサイズ変更
        Hp = PlayerContloller.PlayerHp;
        slider.value = Hp;
    }
}
