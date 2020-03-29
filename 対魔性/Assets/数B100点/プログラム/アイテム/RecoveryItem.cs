using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryItem : MonoBehaviour
{
    GameObject Player;
    //PlayerContloller pc;
    GameManager GameManager;
    [SerializeField] AudioClip SE1 = null;
    public int HealPonit = 1; //回復量
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        //pc = Player.GetComponent<PlayerContloller>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Invoke("Destroy", 10.0f); //10秒後にアイテム削除
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerContloller pc = collision.gameObject.GetComponentInParent<PlayerContloller>();
            if (pc.PlayerHp < pc.MaxPlayerHp) //HPが最大でない
            {
                GameManager.SoundEffect(SE1);
                pc.PlayerHp += HealPonit;
            }
            Destroy(gameObject);
        }
        
    }
}
