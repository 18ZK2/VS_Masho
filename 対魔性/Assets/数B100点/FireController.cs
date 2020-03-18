using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    PlayerContloller player;
    [SerializeField] GameObject thisgo;
    [Header("個数")] public int n;
    [Header("ダメージ")] [SerializeField] float damage = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerContloller>();
        for (int i = 0; i < n; i++)
        {
            GameObject gameObject = Instantiate(thisgo, this.transform.position, Quaternion.identity) as GameObject;
            FireController fireController = gameObject.GetComponent<FireController>(); //生成したゲームオブジェクトのfirecontroller取得
            gameObject.transform.parent = this.gameObject.transform; //自分の子供にする
            gameObject.transform.localPosition = new Vector3(20.0f * (i + 1), 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("good");
        if (collision.tag == "Player")
        {
            player.PlayerHp -= damage;
        }
    }
}
