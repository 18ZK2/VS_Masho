using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryItem : MonoBehaviour
{
    GameObject Player;
    PlayerContloller PlayerContloller;
    GameManager GameManager;
    [SerializeField] AudioClip SE1;
    public int HealPonit = 1; //回復量
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerContloller = Player.GetComponent<PlayerContloller>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log(SE1);
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
                GameManager.SoundEffect(SE1);
                PlayerContloller.PlayerHp += HealPonit;
            }
            Destroy(gameObject);
        }
        
    }
}
