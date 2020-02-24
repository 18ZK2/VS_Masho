using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryItem : MonoBehaviour
{
    GameObject Player;
    PlayerContloller PlayerContloller;
    int HealPonit = 1; //回復量
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerContloller = Player.GetComponent<PlayerContloller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerContloller.PlayerHp < PlayerContloller.MaxPlayerHp) //HPが最大でない
            {
                PlayerContloller.PlayerHp += HealPonit;
            }
            Destroy(gameObject);
        }
        
    }
}
