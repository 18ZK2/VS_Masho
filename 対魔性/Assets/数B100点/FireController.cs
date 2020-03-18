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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContloller>();
        for (int i = 0; i < n; i++)
        {
            GameObject fireball = Instantiate(thisgo, this.transform.position, Quaternion.identity) as GameObject;
            fireball.transform.parent = gameObject.transform; //自分の子供にする
            fireball.transform.localPosition = new Vector3(32.0f * (i + 1), 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.Damage(damage);
        }
    }
}
