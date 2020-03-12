using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJudge : MonoBehaviour
{
    GameObject GameObject;
    Vector2 enemy,player,dir;
    public float r1 = 50f, r2 = 100.0f; //プレイヤーの半径と敵の半径(適当)
    // Start is called before the first frame update
    void Start()
    {
        GameObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        enemy = transform.position;
        player = GameObject.transform.position;
        dir = enemy - player;
        if (dir.magnitude > r1 + r2) //実際のプレイヤーと敵との距離>プレイヤーの半径と敵の半径(適当)
        {
            transform.GetChild(4).gameObject.SetActive(false); //HPバーを見えなくする
        }
        else
        {
            transform.GetChild(4).gameObject.SetActive(true);
        }
    }
}
