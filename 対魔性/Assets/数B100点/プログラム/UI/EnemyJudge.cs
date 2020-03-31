using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyJudge : MonoBehaviour
{
    GameObject GameObject,hpBar;
    Slider hpSlider;
    EnemyContloller ec;
    Vector2 enemy,player,dir;
    public float r = 150f; //プレイヤーの半径と敵の半径(適当)
    // Start is called before the first frame update
    void Start()
    {
        GameObject = GameObject.Find("Player");
        ec = GetComponent<EnemyContloller>();
        hpBar = transform.Find("EnemyHpCanvas").gameObject;
        hpSlider = hpBar.GetComponentInChildren<Slider>();
        hpSlider.maxValue = ec.HP;
    }

    // Update is called once per frame
    void Update()
    {
        
        enemy = transform.position;
        if(GameObject!=null) player = GameObject.transform.position;
        dir = enemy - player;
        
        if (dir.magnitude > r) //実際のプレイヤーと敵との距離>プレイヤーの半径と敵の半径(適当)
        {
            hpBar.SetActive(false); //HPバーを見えなくする
        }
        else
        {
            hpBar.gameObject.SetActive(true);
            hpSlider.value = ec.HP;
        }
    }
}
