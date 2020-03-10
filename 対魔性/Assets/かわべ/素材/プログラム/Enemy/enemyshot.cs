using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyshot : MonoBehaviour
{
    [SerializeField] Transform targetObj;
    private float colliderOffset = 0f;
    private Animator shoter = null;
    public GameObject tumura;
    float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        /*
         * colliderOffsetで自身の当たり判定を引こうとしている？
         */
        //colliderOffset = GetComponent<CapsuleCollider>().radius + targetObj.GetComponent<CapsuleCollider>().radius;
        shoter = GetComponent<Animator>();
        //Vector3 respown_t = GameObject.Find("発射機").transform.position;
        //x = respown_t.x;
        //y = respown_t.y;
        //z = respown_t.z;
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(transform.position, targetObj.position) - colliderOffset;
        //Debug.Log(distance);
        /*
         *測定範囲を可変にしたい場合、条件文の400の部分を変数にすればよい 
         */
        if (distance <= 400)
        {
            shoter.SetBool("distance", true);
        }
        else if (distance > 400)
        {
            shoter.SetBool("distance", false);
        }
    }
    void animation_event()
    {
        Debug.Log("Shot");
        Instantiate(tumura, transform);
        //五連続発射になるのでNG
        //tumuraを入れる配列で管理するとよい
        //倒されるとnullになるのでnullの数を数え、5未満なら生成など
        /*for (int i = 0; i > 5; i++)
        {
            GameObject enemy = Instantiate(tumura,transform);
            //enemy.transform.position = new Vector3(x, y, z);
        }*/
    }
}
