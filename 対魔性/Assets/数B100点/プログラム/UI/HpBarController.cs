using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HpBarController : MonoBehaviour
{
    
    GameObject Player,ThisgameObject;
    PlayerContloller PlayerContloller;
    Slider slider;
    float Width; //HPバーのサイズ
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerContloller = Player.GetComponent<PlayerContloller>();
        ThisgameObject = GameObject.Find("PlayerHealth");
        slider = GameObject.Find("PlayerHealth").GetComponent<Slider>();
        slider.maxValue = PlayerContloller.MaxPlayerHp;
        Width = 800.0f;
    }
    
    // Update is called once per frame
    void Update()
    {
        slider.maxValue = PlayerContloller.MaxPlayerHp;
        if (PlayerContloller.MaxPlayerHp < PlayerContloller.PlayerHp) //最大体力変更時max<NowHpとなったとき
        {
            PlayerContloller.PlayerHp = PlayerContloller.MaxPlayerHp;
        }

        Width = 19.0f * Mathf.Log(PlayerContloller.MaxPlayerHp + 5.0f, 1.10f);  //横幅=最大体力かけ20
        ThisgameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Width,30); //HPサイズ変更
        slider.value = PlayerContloller.PlayerHp;
    }
}
